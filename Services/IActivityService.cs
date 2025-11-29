using HealthTrackAPI.DTOs;

namespace HealthTrackAPI.Services
{
    public interface IActivityService
    {
        Task<ActivityResponse> CreateActivityAsync(ActivityRequest request);
    }
}