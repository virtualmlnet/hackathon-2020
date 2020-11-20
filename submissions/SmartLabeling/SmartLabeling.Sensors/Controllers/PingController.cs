using Microsoft.AspNetCore.Mvc;

namespace SmartLabeling.Sensors.Controllers
{
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new { status = "Sensors IoT device is OK" });
        }
    }
}
