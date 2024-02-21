using HandmadeShop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace HandmadeShop.Context.EntityTypeConfigurations.ProductConfig;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("reviews");

        builder.Property(x => x.Comment).IsRequired();
        builder.Property(x => x.Rating).IsRequired();

        builder.HasOne(x => x.Owner)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.Property(x => x.Images)
            .HasConversion(
                x => JsonConvert.SerializeObject(x),
                x => JsonConvert.DeserializeObject<List<string>>(x) ?? new List<string>());
        
        
    }
}