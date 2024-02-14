using HandmadeShop.Infrastructure.Abstractions.Actions;
using Microsoft.Extensions.DependencyInjection;

namespace HandmadeShop.Services.Action;

public static class DependencyInjection
{
    public static IServiceCollection AddActions(this IServiceCollection services)
    {
        services.AddSingleton<IAction, Action>();

        return services;
    }
}