using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.Context.Factories;

public class DbContextFactory
{
    private readonly DbContextOptions<AppDbContext> _options;

    public DbContextFactory(DbContextOptions<AppDbContext> options)
    {
        _options = options;
    }

    public AppDbContext Create() => new (_options);
}