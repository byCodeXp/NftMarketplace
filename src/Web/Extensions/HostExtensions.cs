using Domain.Entities.Identity;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Web.Extensions;

public static class HostExtensions
{
    public static IHost Seed(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
            
        RoleManager<Role> roleManager = serviceProvider.GetService<RoleManager<Role>>();
            
        new Seed(roleManager).Invoke();
            
        return host;
    }
}