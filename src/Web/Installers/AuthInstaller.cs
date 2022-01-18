using System.Text;
using Installers;
using Installers.Attributes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Web.Installers;

[InstallerOrder(1)]
public class AuthInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        byte[] secret = Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]);
        var signingKey = new SymmetricSecurityKey(secret);

        services.AddTransient(provider => signingKey);
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = signingKey,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
        });
    }
}