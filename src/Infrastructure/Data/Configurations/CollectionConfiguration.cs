using Domain.Entities;
using Infrastructure.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class CollectionConfiguration : EntityConfiguration<CollectionEntity>
{
    public override void Configure(EntityTypeBuilder<CollectionEntity> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(collection => collection.Author)
            .WithMany(user => user.Collections);

        builder
            .HasMany(collection => collection.Tokens)
            .WithOne(token => token.CollectionEntity);
    }
}