using HandmadeShop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeShop.Context.EntityTypeConfigurations.CatalogConfig;

public class CatalogConfiguration : IEntityTypeConfiguration<Catalog>
{
    public void Configure(EntityTypeBuilder<Catalog> builder)
    {
        builder.ToTable("catalogs");

        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

        builder.HasMany(x => x.Products)
            .WithOne(x => x.Catalog)
            .HasForeignKey(x => x.CatalogId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}