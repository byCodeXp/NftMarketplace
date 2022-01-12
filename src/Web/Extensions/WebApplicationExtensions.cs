namespace Web.Extensions;

public static class WebApplicationExtensions
{
    public static void ConfigureHttpRequestPipeline(this WebApplication app)
    {
        var httpRequestPipeline = new HttpRequestPipeline();
        
        httpRequestPipeline.Configure(app);
    }
}