using Domain.Entities.Identity;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Installers.Base;

namespace Web.Installers;

public class DbInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Default"));
        });
        
        services
            .AddIdentity<User, Role>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<DataContext>();
    }
}