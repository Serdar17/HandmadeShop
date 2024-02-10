using HandmadeShop.Services.Settings;

namespace HandmadeShop.Identity;

public static class DependencyInjection
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddMainSettings()
            .AddLogSettings()
            ;

        return services;
    }
}