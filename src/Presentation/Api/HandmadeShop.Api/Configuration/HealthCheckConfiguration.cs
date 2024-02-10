using HandmadeShop.Api.Configuration.HelthChecks;
using HandmadeShop.Common.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace HandmadeShop.Api.Configuration;

public static class HealthCheckConfiguration
{
    public static IServiceCollection AddAppHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<SelfHealthCheck>("NetSchool.API");

        return services;
    }

    public static void UseAppHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health");

        app.MapHealthChecks("/health/detail", new HealthCheckOptions
        {
            ResponseWriter = HealthCheckHelper.WriteHealthCheckResponse,
            AllowCachingResponses = false
        });
    }
}