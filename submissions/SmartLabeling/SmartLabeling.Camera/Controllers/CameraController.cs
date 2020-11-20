using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartLabeling.Core.Models;

namespace SmartLabeling.Camera.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CameraController : ControllerBase
    {
        private readonly ILogger<CameraController> _logger;
        private readonly ApiSettings _settings;

        public CameraController(ILogger<CameraController> logger, ApiSettings settings)
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
