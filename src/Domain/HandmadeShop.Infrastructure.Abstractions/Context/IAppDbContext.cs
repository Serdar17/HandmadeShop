using HandmadeShop.Domain;
using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.Infrastructure.Abstractions.Context;

public interface IAppDbContext : IDisposable
{
    DbSet<Product> Products { get; }
    DbSet<Catalog> Catalogs { get; set; }
    DbSet<Review> Reviews { get; set; }
    DbSet<Like> Likes { get; set; }
    DbSet<User> Users { get; set; }
    
    DbSet<UserLike> UserLikes { get; set; }

    Task SaveAsync(CancellationToken cancellationToken = default);
}