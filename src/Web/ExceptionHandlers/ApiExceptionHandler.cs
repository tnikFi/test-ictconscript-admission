using Common.Constants;
using Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Web.ExceptionHandlers;

public class ApiExceptionHandler : IExceptionHandler
{
    private readonly Dictionary<Type, Func<HttpContext, Exception, CancellationToken, Task>> _exceptionHandlers;

    public ApiExceptionHandler()
    {
        _exceptionHandlers = new ()
        {
            { typeof(NotFoundException), HandleNotFoundExceptionAsync }
        };
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var exceptionType = exception.GetType();
        if (_exceptionHandlers.TryGetValue(exceptionType, out var handler))
        {
            await handler(httpContext, exception, cancellationToken);
            return true;
        }
        return false;
    }

    private static async Task HandleNotFoundExceptionAsync(HttpContext httpContext, Exception ex,
        CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Resource not found.",
            Type = RfcProblemTypes.NotFound,
            Detail = ex.Message
        }, cancellationToken: cancellationToken);
    }
}