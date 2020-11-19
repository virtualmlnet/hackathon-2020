using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace SmartLabeling.API.HealthChecks
{
    public class SensorsHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            //TODO check pings to Sensors API using http client and ping endpoint
            return await Task.Run(() => HealthCheckResult.Healthy("Sensors API is healthy"));
        }
    }
}
