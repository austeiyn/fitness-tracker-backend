using System.ComponentModel.DataAnnotations;

namespace HealthTrackAPI.DTOs
{
    public class UpdateStatusRequest
    {
        [Required]
        public string Status { get; set; } = string.Empty;
    }
}