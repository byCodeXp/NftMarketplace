using Installers;
using Microsoft.OpenApi.Models;
using Web.Swagger;

namespace Web.Installers;

public class SwaggerInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
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
            
            string filePath = Path.Combine(AppContext.BaseDirectory, "Web.xml");
            options.IncludeXmlComments(filePath);
            
            options.DescribeAllParametersInCamelCase();
            
            options.SchemaFilter<EnumSchemaFilter>();
        });
    }
}