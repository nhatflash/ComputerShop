using System;
using ComputerShop.Repository.CustomExceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace ComputerShop.Application.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;   
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError("Exception thrown.");
        var (statusCode, message) = exception switch
        {
            BaseException customEx => (customEx.StatusCode, customEx.Message),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
        };

        httpContext.Response.StatusCode = statusCode;

        var errResponse = new ErrorResponse(statusCode, message);

        await httpContext.Response.WriteAsJsonAsync(errResponse, cancellationToken);

        return true;
    }
}

public class ErrorResponse
{
    public int StatusCode { get; }

    public string Message { get; } = string.Empty;

    public ErrorResponse(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }
}
