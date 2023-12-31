using Blogger.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Blogger.API.Middlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var statusCode = exception switch
        {
            DuplicateEmailException => HttpStatusCode.BadRequest,
            InvalidPasswordException => HttpStatusCode.BadRequest,
            UserNotFoundException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };
        httpContext.Response.StatusCode = (int)statusCode;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = (int)statusCode,
            Type = exception.GetType().Name,
            Title = "An error occured",
            Detail = exception.Message,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
        }, cancellationToken: cancellationToken);

        return true;
    }
}