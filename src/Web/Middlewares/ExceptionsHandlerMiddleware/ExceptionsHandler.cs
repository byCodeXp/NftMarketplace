using System.Net;
using Domain.Exceptions;
using Web.Middlewares.ExceptionsHandlerMiddleware.Models;

namespace Web.Middlewares.ExceptionsHandlerMiddleware;

public class ExceptionsHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionsHandler> _logger;

    public ExceptionsHandler(RequestDelegate next, ILogger<ExceptionsHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception exception)
        {
            string message = exception.Message;
                
            HttpResponse response = context.Response;
            response.ContentType = "application/json";
                
            switch (exception)
            {
                case BadRequestException:
                    response.StatusCode = (int) HttpStatusCode.BadRequest;
                    break;
                default:
                    response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    message = "Internal Server Error";
                    break;
            }

            await response.WriteAsync(new ErrorDetails
            {
                Message = message,
                Code = response.StatusCode
            }.ToJson());
            
            _logger.LogError(exception.ToString());
        }
    }
}