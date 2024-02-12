using Duende.IdentityServer.AspNetIdentity;
using HandmadeShop.Context;
using HandmadeShop.Domain;
using HandmadeShop.Identity.Configuration.IS4;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.Identity.Configuration;

public static class IS4Configuration
{
    public static IServiceCollection AddIS4(this IServiceCollection services)
    {
        services
            .AddIdentity<User, UserRole>(opt =>
            {
                opt.Password.RequiredLength = 0;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;
                // opt.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddUserManager<UserManager<User>>()
            .AddDefaultTokenProviders()
            // .AddTokenProvider<EmailConfirmationTokenProvider<User>>("emailconfirmation")
            ;
        
        // services.Configure<DataProtectionTokenProviderOptions>(opt =>
        //     opt.TokenLifespan = TimeSpan.FromDays(3));
        // services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
        //     opt.TokenLifespan = TimeSpan.FromDays(3));
        

        services
            .AddIdentityServer()
            .AddAspNetIdentity<User>()
            // .AddProfileService<ProfileService<>>()
            .AddInMemoryApiScopes(AppApiScopes.ApiScopes)
            .AddInMemoryClients(AppClients.Clients)
            .AddInMemoryApiResources(AppResources.Resources)
            .AddInMemoryIdentityResources(AppIdentityResources.Resources)

            // .AddTestUsers(AppApiTestUsers.ApiUsers)

            .AddDeveloperSigningCredential();

        return services;
    }

    public static IApplicationBuilder UseIS4(this IApplicationBuilder app)
    {
        app.UseIdentityServer();

        return app;
    }
}