using HandmadeShop.Api.Configuration;
using HandmadeShop.Application;
using HandmadeShop.Services.Logger;
using HandmadeShop.Services.RabbitMq;
using HandmadeShop.Services.Settings;
using HandmadeShop.UseCase.Account;
using HandmadeShop.UseCase.Auth;

namespace HandmadeShop.Api;

/// <summary>
/// 
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration = null)
    {
        services
            .AddMainSettings()
            .AddSwaggerSettings()
            .AddLogSettings()
            .AddIdentitySettings()
            .AddEmailSettings()
            .AddRabbitMq()
            .AddAppHealthChecks()
            .AddAppVersioning()
            .AddAppAutoMappers()
            .AddAppLogger()
            .AddApplication()
            .AddAuthService()
            .AddAccountService()
            ;
        
        return services;
    }
}