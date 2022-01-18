using Installers;

namespace Web.Installers;

public class CorsInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
                policy.AllowAnyOrigin();
            });
        });
    }
}