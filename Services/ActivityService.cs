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

        // UC-003: Create Activity
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

            return MapToResponse(activity);
        }

        // UC-004: Get All Activities
        public async Task<List<ActivityResponse>> GetAllActivitiesAsync(string? type = null, string? status = null)
        {
            var query = _context.Activities.AsQueryable();

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(a => a.Type == type);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(a => a.Status == status);
            }

            var activities = await query
                .OrderByDescending(a => a.Date)
                .ToListAsync();

            return activities.Select(MapToResponse).ToList();
        }

        // UC-005: Get Activity by ID
        public async Task<ActivityResponse> GetActivityByIdAsync(int id)
        {
            var activity = await _context.Activities.FindAsync(id);

            if (activity == null)
            {
                throw new KeyNotFoundException($"Activity with ID {id} not found.");
            }

            return MapToResponse(activity);
        }

        // UC-006: Update Activity
        public async Task<ActivityResponse> UpdateActivityAsync(int id, ActivityRequest request)
        {
            var activity = await _context.Activities.FindAsync(id);

            if (activity == null)
            {
                throw new KeyNotFoundException($"Activity with ID {id} not found.");
            }

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

            activity.UserId = request.UserId;
            activity.Type = request.Type;
            activity.Name = request.Name;
            activity.Description = request.Description;
            activity.Date = request.Date;
            activity.Status = request.Status;
            activity.DurationMinutes = request.DurationMinutes;
            activity.Calories = request.Calories;
            activity.StepsCount = request.StepsCount;
            activity.MealType = request.MealType;
            activity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return MapToResponse(activity);
        }

        // UC-007: Update Activity Status
        public async Task<bool> UpdateActivityStatusAsync(int id, string status)
        {
            var activity = await _context.Activities.FindAsync(id);

            if (activity == null)
            {
                throw new KeyNotFoundException($"Activity with ID {id} not found.");
            }

            var validStatuses = new[] { "Planned", "InProgress", "Completed" };
            if (!validStatuses.Contains(status))
            {
                throw new ArgumentException("Invalid status. Must be Planned, InProgress, or Completed.");
            }

            activity.Status = status;
            activity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        // UC-008: Delete Activity
        public async Task<bool> DeleteActivityAsync(int id)
        {
            var activity = await _context.Activities.FindAsync(id);

            if (activity == null)
            {
                throw new KeyNotFoundException($"Activity with ID {id} not found.");
            }

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();

            return true;
        }

        // UC-009: Get User Activities
        public async Task<List<ActivityResponse>> GetUserActivitiesAsync(int userId)
        {
            var activities = await _context.Activities
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.Date)
                .ToListAsync();

            return activities.Select(MapToResponse).ToList();
        }

        // UC-010: Get Activities by Date Range
        public async Task<List<ActivityResponse>> GetActivitiesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new ArgumentException("End date must be greater than or equal to start date.");
            }

            var activities = await _context.Activities
                .Where(a => a.Date >= startDate && a.Date <= endDate)
                .OrderByDescending(a => a.Date)
                .ToListAsync();

            return activities.Select(MapToResponse).ToList();
        }

        // Helper method to map Activity to ActivityResponse
        private ActivityResponse MapToResponse(Activity activity)
        {
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