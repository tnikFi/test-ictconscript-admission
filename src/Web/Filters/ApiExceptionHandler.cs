using Common.Constants;
using Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Web.Filters;

public class ApiExceptionHandler : IExceptionHandler
{
    private readonly Dictionary<Type, Func<HttpContext, Exception, CancellationToken, Task>> _exceptionHandlers;

    public ApiExceptionHandler()
    {
        _exceptionHandlers = new ()
        {
            { typeof(ValidationException), HandleValidationExceptionAsync },
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

    private static async Task HandleValidationExceptionAsync(HttpContext httpContext, Exception ex,
        CancellationToken cancellationToken)
    {
        var exception = (ValidationException)ex;
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "One or more validation errors occurred.",
            Type = RfcProblemTypes.BadRequest,
            Detail = exception.Errors.First().ErrorMessage,
            Extensions = new Dictionary<string, object?>
            {
                ["errors"] = exception?.Errors
            }
        }, cancellationToken: cancellationToken);
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