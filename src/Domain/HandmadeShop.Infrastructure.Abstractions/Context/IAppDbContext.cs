using HandmadeShop.Domain;
using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.Infrastructure.Abstractions.Context;

public interface IAppDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Catalog> Catalogs { get; set; }
    DbSet<Review> Reviews { get; set; }
    DbSet<Like> Likes { get; set; }

    Task SaveAsync(CancellationToken cancellationToken = default);
}