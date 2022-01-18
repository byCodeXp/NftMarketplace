using Application;
using FluentValidation;
using Infrastructure;
using Installers;
using MediatR;
using Microsoft.OpenApi.Models;
using Web.Helpers;
using Web.Services;

namespace Web.Installers;

public class MvcInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        
        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "NftMarketplace API", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        
        services.AddTransient<IIdentityService, IdentityService>();

        services.AddTransient<JwtHelper>();

        services.AddMediatR(typeof(BaseRequest), typeof(IRequestHandler<,>));

        services.AddValidatorsFromAssemblyContaining(typeof(Program), ServiceLifetime.Transient);
    }
}