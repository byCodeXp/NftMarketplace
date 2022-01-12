using FluentValidation;
using Infrastructure;
using MediatR;
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
        
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.AddTransient<IIdentityService, IdentityService>();

        services.AddTransient<JwtHelper>();

        services.AddMediatR(typeof(Program));

        services.AddValidatorsFromAssemblyContaining(typeof(Program), ServiceLifetime.Transient);
    }
}