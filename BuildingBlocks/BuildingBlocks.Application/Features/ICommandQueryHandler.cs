using System;
using MediatR;
/*
Çok güzel ve modern bir MediatR interface’i paylaşmışsın!
Bunu “CQRS handler katmanı” olarak düşünebilirsin:
Herhangi bir Command veya Query request’ini işleyip, sonuç olarak Result veya Result<T> döndürmek için arayüzler tanımlıyorsun.
*/

namespace BuildingBlocks.Application.Features;

public interface ICommandQueryHandler<in TRequest, TResult> : IRequestHandler<TRequest, Result<TResult>> 
    where TRequest : IRequest<Result<TResult>>
{
}

public interface ICommandQueryHandler<in TRequest> : IRequestHandler<TRequest, Result> 
    where TRequest : IRequest<Result>
{
}