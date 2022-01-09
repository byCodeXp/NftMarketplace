using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Data.Identity;

namespace Web.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasMany(user => user.Tokens)
            .WithOne(token => token.Author);

        builder
            .HasMany(user => user.Collections)
            .WithOne(collection => collection.Author);
    }
}