using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthCheck.TestingWebApp;

public class TestingHealthCheck : IHealthCheck
{
    internal static volatile bool IsHealthy = false;

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        HealthCheckResult result;

        if (IsHealthy)
        {
            result = HealthCheckResult.Healthy("A healthy result.");
        }
        else
        {
            result = HealthCheckResult.Unhealthy("A unhealthy result.");
        }

        return Task.FromResult(result);
    }

#if TEST_WITH_TIMMER
    private static readonly Timer? Timer = new(DoCheck, null, 5000, 5000);

    private static void DoCheck(object? stateInfo)
    {
        IsHealthy = true;
        Timer?.Dispose();
    }
#endif
}
