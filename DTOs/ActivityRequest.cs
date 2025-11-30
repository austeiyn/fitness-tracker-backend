using System.ComponentModel.DataAnnotations;

namespace HealthTrackAPI.DTOs
{
    public class ActivityRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Type { get; set; } = string.Empty; // "Workout", "Meal", "Steps"

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        public string Status { get; set; } = "Planned"; // "Planned", "InProgress", "Completed"

        // Workout-specific
        public int? DurationMinutes { get; set; }
        
        // Workout & Meal
        public int? Calories { get; set; }

        // Steps-specific
        public int? StepsCount { get; set; }

        // Meal-specific
        public string? MealType { get; set; } // "Breakfast", "Lunch", "Dinner", "Snack"
    }
}