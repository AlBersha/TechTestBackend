using System.Text.Json;
using Microsoft.AspNetCore.Http;
using TechTestBackend.Domain.Exceptions;

namespace TechTestBackend.Domain.Middleware;

public class ErrorHandlerMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception switch
        {
            BaseException baseException => (int)baseException.HttpStatusCode,
            _ => StatusCodes.Status500InternalServerError 
        };

        var response = new
        {
            context.Response.StatusCode, exception.Message
        };
        
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}