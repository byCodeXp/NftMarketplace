using Domain.Entities.Identity;
using Infrastructure.Data;
using Installers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.Installers;

public class DbInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IDataContext, DataContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Default"));
        });
        
        services
            .AddIdentity<User, Role>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<DataContext>();
    }
}