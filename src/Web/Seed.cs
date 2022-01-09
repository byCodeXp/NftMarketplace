using Microsoft.AspNetCore.Identity;
using Web.Data.Identity;

namespace Web;

public class Seed
{
    private readonly RoleManager<Role> _roleManager;

    public Seed(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public void Invoke()
    {
        Task.Run(async () =>
        {
            if (!await _roleManager.RoleExistsAsync(Env.Roles.User))
            {
                await _roleManager.CreateAsync(new Role(Env.Roles.User));
            }
        }).Wait();
    }
}
