using Domain;
using Microsoft.Extensions.FileProviders;
using Web.Middlewares.ExceptionsHandlerMiddleware.Extensions;

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

        app.UseExceptionsHandler();

        app.UseCors();
        
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        
        
        var path = Path.Combine(Directory.GetCurrentDirectory(), Env.Storage.Path);
        
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(path),
            RequestPath = "/cdn"
        });
    }
}