using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace HandmadeShop.UseCase.Auth;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthService(this IServiceCollection services)
    {
        services.AddMediatR(
            cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        return services;
    }
}