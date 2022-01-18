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
        
        
        // Setup storage
        
        if (!Directory.Exists(Env.Storage.TokenPicturePath))
        {
            Directory.CreateDirectory(Env.Storage.TokenPicturePath);
        }
        
        if (!Directory.Exists(Env.Storage.CollectionPicturePath))
        {
            Directory.CreateDirectory(Env.Storage.CollectionPicturePath);
        }

        string collectionPicturesPath = Path.Combine(Directory.GetCurrentDirectory(), Env.Storage.CollectionPicturePath);
        string tokenPicturesPath = Path.Combine(Directory.GetCurrentDirectory(), Env.Storage.TokenPicturePath);
        
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(tokenPicturesPath),
            RequestPath = "/cdn/tokens/pictures"
        });
        
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(collectionPicturesPath),
            RequestPath = "/cdn/collections/pictures"
        });
    }
}