﻿using Duende.IdentityServer.Services;

namespace HandmadeShop.Identity.Configuration;

/// <summary>
/// CORS configuration
/// </summary>
public static class CorsConfiguration
{
    /// <summary>
    /// Add CORS
    /// </summary>
    /// <param name="services">Services collection</param>
    public static IServiceCollection AddAppCors(this IServiceCollection services)
    {
        services.AddSingleton<ICorsPolicyService>((container) =>
        {
            var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();

            return new DefaultCorsPolicyService(logger)
            {
                AllowAll = true
            };
        });

        return services;
    }

    /// <summary>
    /// Use service
    /// </summary>
    /// <param name="app">Application</param>
    public static void UseAppCors(this IApplicationBuilder app)
    {
        app.UseCors(pol =>
        {
            pol.AllowAnyHeader();
            pol.AllowAnyMethod();
            pol.AllowCredentials();
            pol.SetIsOriginAllowed(origin => true);
            pol.WithExposedHeaders("Content-Disposition");
        });
    }
}