using Domain.Entities;
using Infrastructure.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

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