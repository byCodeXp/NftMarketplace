using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Data.Configurations.Base;
using Web.Data.Entities;

namespace Web.Data.Configurations;

public class CollectionConfiguration : EntityConfiguration<Collection>
{
    public override void Configure(EntityTypeBuilder<Collection> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(collection => collection.Author)
            .WithMany(user => user.Collections);

        builder
            .HasMany(collection => collection.Tokens)
            .WithOne(token => token.Collection);
    }
}