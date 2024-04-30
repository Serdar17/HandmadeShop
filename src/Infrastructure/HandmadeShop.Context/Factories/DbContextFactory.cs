using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.Context.Factories;

public class DbContextFactory(DbContextOptions<AppDbContext> options)
{
    public AppDbContext Create() => new (options);
}