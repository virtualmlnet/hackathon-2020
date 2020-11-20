using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;
using System.Threading.Tasks;

namespace SmartLabeling.API.Controllers
{
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly HealthCheckService _healthCheckService;

        public HealthController(HealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new { status = "Main application is OK." });
        }

        //[HttpGet("health")]
        //public async Task<IActionResult> HealthAsync()
        //{
        //    var report = await _healthCheckService.CheckHealthAsync();

        //    return report.Status == HealthStatus.Healthy 
        //        ? Ok(report)
        //        : StatusCode((int)HttpStatusCode.ServiceUnavailable, report);
        //}
    }
}
