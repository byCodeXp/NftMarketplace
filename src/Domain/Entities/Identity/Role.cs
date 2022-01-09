using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity;

public class Role : IdentityRole<Guid>
{
    public Role(string name)
        : base(name)
    {
    }
}