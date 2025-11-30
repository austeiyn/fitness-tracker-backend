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

        // UC-003: Create Activity
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
                return CreatedAtAction(nameof(GetActivityById), new { id = response.Id }, response);
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

        // UC-004: Get All Activities
        [HttpGet]
        public async Task<IActionResult> GetAllActivities([FromQuery] string? type = null, [FromQuery] string? status = null)
        {
            try
            {
                var activities = await _activityService.GetAllActivitiesAsync(type, status);
                return Ok(activities);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // UC-005: Get Activity by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityById(int id)
        {
            try
            {
                var activity = await _activityService.GetActivityByIdAsync(id);
                return Ok(activity);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}