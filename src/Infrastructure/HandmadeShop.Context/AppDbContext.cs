using HandmadeShop.Domain;
using HandmadeShop.Infrastructure.Abstractions.Context;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.Context;

public class AppDbContext : IdentityDbContext<User,  UserRole, Guid>, IAppDbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Catalog> Catalogs { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<UserLike> UserLikes { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Buyer> Buyers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
    
    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await SaveChangesAsync(cancellationToken);
    }
}