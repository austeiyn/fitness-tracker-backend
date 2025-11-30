using Microsoft.AspNetCore.Mvc;
using HealthTrackAPI.DTOs;
using HealthTrackAPI.Services;

namespace HealthTrackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivitiesController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody] ActivityRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _activityService.CreateActivityAsync(request);
                return CreatedAtAction(nameof(CreateActivity), new { id = response.Id }, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}