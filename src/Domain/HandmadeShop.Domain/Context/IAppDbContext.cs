using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.Domain;

public interface IAppDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Catalog> Catalogs { get; set; }
    DbSet<Review> Reviews { get; set; }
    DbSet<Like> Likes { get; set; }

    Task SaveAsync(CancellationToken cancellationToken = default);
}