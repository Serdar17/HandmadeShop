﻿using HandmadeShop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace HandmadeShop._Context.EntityTypeConfigurations.ProductConfig;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("reviews");

        builder.Property(x => x.Comment).IsRequired();
        builder.Property(x => x.Rating).IsRequired();
        builder.Property(x => x.Rating).IsRequired();
        
        builder.Property(x => x.Images)
            .HasConversion(
                x => JsonConvert.SerializeObject(x),
                x => JsonConvert.DeserializeObject<List<string>>(x) ?? new List<string>());
        
        
    }
}