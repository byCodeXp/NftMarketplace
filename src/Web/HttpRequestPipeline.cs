using Web.Middlewares;

namespace Web;

public class HttpRequestPipeline
{
    public void Configure(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        

        app.UseMiddleware<ErrorsHandlerMiddleware>();

        app.UseCors();
        
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
    }
}