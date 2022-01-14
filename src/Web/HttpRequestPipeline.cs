using Domain;
using Microsoft.Extensions.FileProviders;
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
        
        
        var path = Path.Combine(Directory.GetCurrentDirectory(), Env.Storage.Path);
        
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(path),
            RequestPath = "/cdn"
        });
    }
}