using HandmadeShop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeShop._Context.EntityTypeConfigurations.ProductConfig;

public class SpecificationConfiguration : IEntityTypeConfiguration<Specification>
{
    public void Configure(EntityTypeBuilder<Specification> builder)
    {
        builder.ToTable("specifications");

        builder.Property(x => x.Name).HasMaxLength(250);
        builder.Property(x => x.Value).HasMaxLength(250);
    }
}