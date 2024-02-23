using HandmadeShop.Infrastructure.Abstractions.Caching;
using Microsoft.Extensions.DependencyInjection;

namespace HandmadeShop.Services.Cache;

public static  class DependencyInjection
{
    public static IServiceCollection AddCacheService(this IServiceCollection services)
    {
        services.AddSingleton<ICacheService, CacheService>();
        
        return services;
    }
}