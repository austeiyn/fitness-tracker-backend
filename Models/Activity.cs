using System.ComponentModel.DataAnnotations;

namespace HealthTrackAPI.Models
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Type { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        public string Status { get; set; } = "Planned";

        public int? DurationMinutes { get; set; }

        public int? Calories { get; set; }

        public int? StepsCount { get; set; }

        public string? MealType { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation property
        public User? User { get; set; }
    }
}