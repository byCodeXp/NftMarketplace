using System.Net;
using Application.Exceptions;
using Web.Endpoints.Responses;

namespace Web.Middlewares;

public class ErrorsHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorsHandlerMiddleware> _logger;

    public ErrorsHandlerMiddleware(RequestDelegate next, ILogger<ErrorsHandlerMiddleware> logger)
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
            string message = null;
                
            HttpResponse response = context.Response;
            response.ContentType = "application/json";
                
            switch (exception)
            {
                case BadRequestException:
                    response.StatusCode = (int) HttpStatusCode.BadRequest;
                    message = exception.Message;
                    break;
                default:
                    response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    message = "Internal Server Error";
                    break;
            }

            _logger.LogError(exception.ToString());

            await response.WriteAsync(new FailedResponse
            {
                Code = response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}