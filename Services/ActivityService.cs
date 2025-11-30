using HealthTrackAPI.Data;
using HealthTrackAPI.DTOs;
using HealthTrackAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthTrackAPI.Services
{
    public class ActivityService : IActivityService
    {
        private readonly HealthTrackContext _context;

        public ActivityService(HealthTrackContext context)
        {
            _context = context;
        }

        public async Task<ActivityResponse> CreateActivityAsync(ActivityRequest request)
        {
            // Validate activity type
            var validTypes = new[] { "Workout", "Meal", "Steps" };
            if (!validTypes.Contains(request.Type))
            {
                throw new ArgumentException("Invalid activity type. Must be Workout, Meal, or Steps.");
            }

            // Validate status
            var validStatuses = new[] { "Planned", "InProgress", "Completed" };
            if (!validStatuses.Contains(request.Status))
            {
                throw new ArgumentException("Invalid status. Must be Planned, InProgress, or Completed.");
            }

            var activity = new Activity
            {
                UserId = request.UserId,
                Type = request.Type,
                Name = request.Name,
                Description = request.Description,
                Date = request.Date,
                Status = request.Status,
                DurationMinutes = request.DurationMinutes,
                Calories = request.Calories,
                StepsCount = request.StepsCount,
                MealType = request.MealType,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();

            return new ActivityResponse
            {
                Id = activity.Id,
                UserId = activity.UserId,
                Type = activity.Type,
                Name = activity.Name,
                Description = activity.Description,
                Date = activity.Date,
                Status = activity.Status,
                DurationMinutes = activity.DurationMinutes,
                Calories = activity.Calories,
                StepsCount = activity.StepsCount,
                MealType = activity.MealType,
                CreatedAt = activity.CreatedAt,
                UpdatedAt = activity.UpdatedAt
            };
        }
    }
}