using HandmadeShop.Web.Pages.Auth.Services;
using HandmadeShop.Web.Pages.Product.Services;
using HandmadeShop.Web.Pages.Profile.Services;
using HandmadeShop.Web.Providers;
using HandmadeShop.Web.Services;
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
            .AddScoped<IProductService, ProductService>();
        
        
        return services;
    }
}