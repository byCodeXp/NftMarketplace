namespace Web.Middlewares.ExceptionsHandlerMiddleware.Extensions;

public static class WebApplicationExtensions
{
    public static void UseExceptionsHandler(this WebApplication app)
    {
        app.UseMiddleware<ExceptionsHandler>();
    }
}