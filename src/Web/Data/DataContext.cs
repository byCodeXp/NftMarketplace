using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Data.Identity;

namespace Web.Data;

public class DataContext : IdentityDbContext<User, Role, Guid>
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }
}