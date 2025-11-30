using HealthTrackAPI.DTOs;

namespace HealthTrackAPI.Services
{
    public interface IActivityService
    {
        Task<ActivityResponse> CreateActivityAsync(ActivityRequest request);
        Task<List<ActivityResponse>> GetAllActivitiesAsync(string? type = null, string? status = null);
    }
}