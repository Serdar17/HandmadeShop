using HandmadeShop.Api.Configuration;
using HandmadeShop.Services.Logger;
using HandmadeShop.Services.RabbitMq;
using HandmadeShop.Services.Settings;

namespace HandmadeShop.Api;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration = null)
    {
        services
            .AddMainSettings()
            .AddSwaggerSettings()
            .AddLogSettings()
            .AddIdentitySettings()
            .AddRabbitMq()
            .AddAppHealthChecks()
            .AddAppVersioning()
            .AddAppAutoMappers()
            .AddAppLogger()
            ;
        
        
        return services;
    }
}