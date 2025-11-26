using Microsoft.AspNetCore.Mvc;

namespace HealthTrackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                status = "healthy",
                message = "HealthTrack API running",
                timestamp = DateTime.UtcNow
            });
        }
    }
}