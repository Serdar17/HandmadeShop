using HandmadeShop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeShop.Context.EntityTypeConfigurations.OrderConfig;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("order_items");
        
        builder.Property(x => x.ProductName).IsRequired().HasMaxLength(250);
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.ImagePath).HasMaxLength(250);
    }
}