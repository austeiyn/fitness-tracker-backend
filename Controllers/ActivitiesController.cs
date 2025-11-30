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

        // UC-006: Update Activity
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActivity(int id, [FromBody] ActivityRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var activity = await _activityService.UpdateActivityAsync(id, request);
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

        // UC-007: Update Activity Status
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateActivityStatus(int id, [FromBody] UpdateStatusRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _activityService.UpdateActivityStatusAsync(id, request.Status);
                return Ok(new { message = "Status updated successfully" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
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

        // UC-008: Delete Activity
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            try
            {
                await _activityService.DeleteActivityAsync(id);
                return NoContent();
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

        // UC-009: Get User Activities
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserActivities(int userId)
        {
            try
            {
                var activities = await _activityService.GetUserActivitiesAsync(userId);
                return Ok(activities);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // UC-010: Get Activities by Date Range
        [HttpGet("range")]
        public async Task<IActionResult> GetActivitiesByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var activities = await _activityService.GetActivitiesByDateRangeAsync(startDate, endDate);
                return Ok(activities);
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