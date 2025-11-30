using HealthTrackAPI.DTOs;

namespace HealthTrackAPI.Services
{
    public interface IActivityService
    {
        Task<ActivityResponse> CreateActivityAsync(ActivityRequest request);
        Task<List<ActivityResponse>> GetAllActivitiesAsync(string? type = null, string? status = null);
        Task<ActivityResponse> GetActivityByIdAsync(int id);
        Task<ActivityResponse> UpdateActivityAsync(int id, ActivityRequest request);
        Task<bool> UpdateActivityStatusAsync(int id, string status);
        Task<bool> DeleteActivityAsync(int id);
        Task<List<ActivityResponse>> GetUserActivitiesAsync(int userId);
        Task<List<ActivityResponse>> GetActivitiesByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}