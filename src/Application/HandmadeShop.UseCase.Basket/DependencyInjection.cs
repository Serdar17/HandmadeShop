using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace HandmadeShop.UseCase.Basket;

public static class DependencyInjection
{
    public static IServiceCollection AddBasketService(this IServiceCollection services)
    {
        services.AddMediatR(
            cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        return services;
    }
}