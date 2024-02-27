using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace HandmadeShop.UseCase.Order;

public static class DependencyInjection
{
    public static IServiceCollection AddOrderService(this IServiceCollection services)
    {
        services.AddMediatR(
            cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
       
        return services;
    }
}