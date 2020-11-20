using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace SmartLabeling.API.HealthChecks
{
    public class FakeHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            return await Task.Run(() => HealthCheckResult.Healthy("Fake is healthy"));
        }
    }
}
