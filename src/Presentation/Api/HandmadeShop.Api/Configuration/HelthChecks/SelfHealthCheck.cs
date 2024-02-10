using System.Reflection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HandmadeShop.Api.Configuration.HelthChecks;

public class SelfHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var assembly = Assembly.Load("HandmadeShop.Api");
        var versionNumber = assembly.GetName().Version;

        return Task.FromResult(HealthCheckResult.Healthy($"Build {versionNumber}"));
    }
}