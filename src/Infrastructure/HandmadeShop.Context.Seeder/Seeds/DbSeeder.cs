using HandmadeShop.Domain;
using HandmadeShop.UseCase.Auth.Commands.RegisterUser;
using HandmadeShop.UseCase.Auth.Models;
using Microsoft.AspNetCore.Identity;
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
    
    private static UserManager<User> UserManager(IServiceProvider serviceProvider)
    {
        return ServiceScope(serviceProvider)
            .ServiceProvider.GetRequiredService<UserManager<User>>();
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

        using var userManager = UserManager(serviceProvider);
        await using var context = DbContext(serviceProvider);

        if (await context.Catalogs.AnyAsync())
            return;
        
        if (await context.Products.AnyAsync())
            return;
        
        if (await context.Users.AnyAsync())
            return;

        var catalogs = SeedData.TestCatalogs;
        catalogs[0].Products = SeedData.TestProduct1;
        catalogs[1].Products = SeedData.TestProduct2;
        catalogs[2].Products = SeedData.TestProduct3;
        catalogs[3].Products = SeedData.TestProduct4;
        
        context.Catalogs.AddRange(catalogs);
        
        var seller = SeedData.Seller;
        seller.PasswordHash = userManager.PasswordHasher.HashPassword(seller, seller.PasswordHash);
        
        var buyer = SeedData.Buyer;
        buyer.PasswordHash = userManager.PasswordHasher.HashPassword(buyer, buyer.PasswordHash);
        
        seller.Products = SeedData.TestProducts;
        context.Users.Add(seller);
        context.Users.Add(buyer);
        
        await context.SaveChangesAsync();
    }
}