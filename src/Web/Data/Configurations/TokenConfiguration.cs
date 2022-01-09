using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Data.Configurations.Base;
using Web.Data.Entities;

namespace Web.Data.Configurations;

public class TokenConfiguration : EntityConfiguration<Token>
{
    public override void Configure(EntityTypeBuilder<Token> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(token => token.Author)
            .WithMany(user => user.Tokens);

        builder
            .HasOne(token => token.Collection)
            .WithMany(collection => collection.Tokens);
    }
}