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
            .AddScoped<BasketTransferService>();
        
        
        return services;
    }
}