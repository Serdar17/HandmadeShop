using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HandmadeShop._Context;

public static class DependencyInjection
{
    public static IServiceCollection AddAppDbContext(
        this IServiceCollection services,
        IConfiguration? configuration = null)
    {
        return services;
    }
}