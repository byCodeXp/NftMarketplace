using Microsoft.AspNetCore.Identity;

namespace Web.Data.Identity;

public class Role : IdentityRole<Guid>
{
    public Role(string name)
        : base(name)
    {
    }
}