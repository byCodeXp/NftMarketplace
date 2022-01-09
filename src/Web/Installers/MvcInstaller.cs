using Web.Helpers;
using Web.Installers.Base;
using Web.Services;

namespace Web.Installers;

public class MvcInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        
        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen();
        
        services.AddTransient<JwtHelper>();
        services.AddTransient<IdentityService>();
    }
}