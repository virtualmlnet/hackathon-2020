using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartLabeling.Core.Models;

namespace SmartLabeling.Sensors.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorsController : ControllerBase
    {
        private readonly ILogger<SensorsController> _logger;
        private readonly ApiSettings _settings;

        public SensorsController(ILogger<SensorsController> logger, ApiSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation($"GET triggered.");

            return Ok();
        }
    }
}
