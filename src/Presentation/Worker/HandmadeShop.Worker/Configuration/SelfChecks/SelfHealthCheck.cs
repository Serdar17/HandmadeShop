using System.Reflection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HandmadeShop.Worker.Configuration.SelfChecks;

public class SelfHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var assembly = Assembly.Load("HandmadeShop.Worker");
        var versionNumber = assembly.GetName().Version;

        return Task.FromResult(HealthCheckResult.Healthy(description: $"Build {versionNumber}"));
    }
}