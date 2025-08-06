using System;
using FluentValidation;
using MediatR;
using FluentValidation.Results;
using Humanizer;
using BuildingBlocks.Application.Exceptions;


namespace BuildingBlocks.Application.Behaviors;
/*
Her MediatR isteğinde (Command/Query),
ilgili validator varsa otomatik olarak devreye girer.

İstek modelini kontrol eder,

Eğer hata varsa:

Hataları BadRequestException ile fırlatır (ve API hemen döner).

Hata yoksa:

İstek pipeline’da ilerler (işlem yapılır).
*/
public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken = default)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResult = await validators.First().ValidateAsync(context, cancellationToken);

            if (!validationResult.IsValid)
            {
                var failures = Serialize(validationResult.Errors);
                throw new BadRequestException(Resources.Messages.BadRequest, failures);
            }
        }
        return await next();
    }
    private static Dictionary<string, string[]> Serialize(IEnumerable<ValidationFailure> failures)
    {
        var camelCaseFailures = failures
            .GroupBy(failure => failure.PropertyName.Camelize())
            .ToDictionary(
                group => group.Key,
                group => group.Select(failures => failures.ErrorMessage).ToArray()
            );
        return camelCaseFailures;
    }
}
