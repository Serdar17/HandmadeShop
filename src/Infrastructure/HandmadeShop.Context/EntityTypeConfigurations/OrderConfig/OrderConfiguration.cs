using HandmadeShop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeShop.Context.EntityTypeConfigurations.OrderConfig;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");

        builder.Property(x => x.Description)
            .HasMaxLength(1000).IsRequired();

        builder.OwnsOne(x => x.Address);

        builder.HasOne(x => x.Buyer)
            .WithOne(x => x.Order)
            .HasForeignKey<Order>(x => x.BuyerId);

        builder.HasMany(x => x.Items)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId);
    }
}