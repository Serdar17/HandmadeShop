using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace HandmadeShop.UseCase.Account;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountService(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        return services;
    }
}