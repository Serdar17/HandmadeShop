using HandmadeShop.Api.Configuration;
using HandmadeShop.Api.Services;
using HandmadeShop.Application;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.Services.Action;
using HandmadeShop.Services.Cache;
using HandmadeShop.Services.EmailSender;
using HandmadeShop.Services.FileStorage;
using HandmadeShop.Services.Logger;
using HandmadeShop.Services.RabbitMq;
using HandmadeShop.Services.Settings;
using HandmadeShop.UseCase.Account;
using HandmadeShop.UseCase.Auth;
using HandmadeShop.UseCase.Basket;
using HandmadeShop.UseCase.Review;
using HandmadeShop.UserCase.Catalog;

namespace HandmadeShop.Api;

/// <summary>
/// Register all application services
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Extension for DI <see cref="IServiceCollection"/>
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    /// <param name="configuration">IConfiguration</param>
    /// <returns></returns>
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration = null)
    {
        services
            .AddMainSettings()
            .AddSwaggerSettings()
            .AddLogSettings()
            .AddIdentitySettings()
            .AddEmailSettings()
            .AddFileStorageSettings()
            .AddWebSettings()
            .AddCacheSettings()
            .AddRabbitMq()
            .AddAppHealthChecks()
            .AddAppVersioning()
            .AddAppAutoMappers()
            .AddAppLogger()
            .AddApplication()
            .AddAuthService()
            .AddAccountService()
            .AddActions()
            .AddAppFileStorage()
            .AddEmailSender()
            .AddCatalogService()
            .AddReviewService()
            .AddCacheService()
            .AddBasketService()
            ;

        services.AddTransient<IIdentityService, IdentityService>();
        return services;
    }
}