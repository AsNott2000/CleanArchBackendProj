using BuildingBlocks.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using BuildingBlocks.Resources;

namespace BuildingBlocks.Application.Middlewares;

public class HttpResponseMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {

        await next(context);

        switch (context.Response.StatusCode)
        {
            case StatusCodes.Status405MethodNotAllowed:
                throw new MethodNotAllowedException(Messages.MethodNotAllowed);

            case StatusCodes.Status401Unauthorized:
                throw new UnauthorizedException(Messages.Unauthorized);

            case StatusCodes.Status403Forbidden:
                throw new ForbiddenException(Messages.Forbidden);

            // Add more cases as needed
        }
    }
}
