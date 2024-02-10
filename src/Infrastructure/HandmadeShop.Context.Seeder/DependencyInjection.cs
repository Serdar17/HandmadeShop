using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HandmadeShop.Context.Seeder;

public static class DependencyInjection
{
    public static IServiceCollection AddAppSeeder(this IServiceCollection services, IConfiguration? configuration = null)
    {
        return services;
    }
}