using HandmadeShop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeShop.Context.EntityTypeConfigurations.ProductConfig;

public class UserLikeConfiguration : IEntityTypeConfiguration<UserLike>
{
    public void Configure(EntityTypeBuilder<UserLike> builder)
    {
        builder.ToTable("users_likes");

        builder.HasKey(x => new { x.LikeId, x.UserId });

        builder.HasOne(x => x.User)
            .WithMany(x => x.UserLikes)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Like)
            .WithMany(x => x.UserLikes)
            .HasForeignKey(x => x.LikeId);
    }
}