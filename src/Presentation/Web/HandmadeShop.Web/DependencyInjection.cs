using HandmadeShop.Web.Handlers;
using HandmadeShop.Web.Pages.Auth.Services;
using HandmadeShop.Web.Pages.Basket.Services;
using HandmadeShop.Web.Pages.Order.Services;
using HandmadeShop.Web.Pages.Product.Services;
using HandmadeShop.Web.Pages.Profile.Services;
using HandmadeShop.Web.Pages.Review.Services;
using HandmadeShop.Web.Providers;
using HandmadeShop.Web.Services;
using HandmadeShop.Web.TransferServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace HandmadeShop.Web;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<IIdentityService, IdentityService>()
            .AddScoped<IProductService, ProductService>()
            .AddScoped<IReviewService, ReviewService>()
            .AddScoped<IClipboardService, ClipboardService>()
            .AddScoped<IBasketService, BasketService>()
            .AddScoped<IOrderService, OrderService>()
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<IConfigurationService, ConfigurationService>()
            .AddScoped<BasketTransferService>();
        
        
        return services;
    }

    public static IServiceCollection AddHttpClients(this IServiceCollection services, WebAssemblyHostBuilder builder)
    {
        builder.Services.AddTransient<AuthenticationDelegatingHandler>();
        
        builder.Services.AddHttpClient(Settings.Api)
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
            .AddHttpMessageHandler<AuthenticationDelegatingHandler>();

        builder.Services.AddHttpClient(Settings.Identity)
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(Settings.IdentityRoot));

        return services;
    }
}