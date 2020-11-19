using Microsoft.AspNetCore.Mvc;

namespace SmartLabeling.Camera.Controllers
{
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new { status = "Camera IoT device is OK." });
        }
    }
}
