using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HandmadeShop.Context.Seeder.Seeds;

public static class DbSeeder
{
    private static IServiceScope ServiceScope(IServiceProvider serviceProvider)
    {
        return serviceProvider.GetService<IServiceScopeFactory>()!.CreateScope();
    }

    private static AppDbContext DbContext(IServiceProvider serviceProvider)
    {
        return ServiceScope(serviceProvider)
            .ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>().CreateDbContext();
    }

    public static void Execute(IServiceProvider serviceProvider)
    {
        Task.Run(async () =>
            {
                await AddDemoData(serviceProvider);
            })
            .GetAwaiter()
            .GetResult();
    }

    private static async Task AddDemoData(IServiceProvider serviceProvider)
    {
        using var scope = ServiceScope(serviceProvider);
        if (scope is null)
            return;

        await using var context = DbContext(serviceProvider);

        if (await context.Catalogs.AnyAsync())
            return;
        
        if (await context.Users.AnyAsync())
            return;
        
        context.Users.Add(SeedData.Seller);

        if (await context.Products.AnyAsync())
            return;

        await context.Products.AddRangeAsync(SeedData.TestProducts);
        
        await context.SaveChangesAsync();
    }
}