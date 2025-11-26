namespace HealthTrackAPI.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; } = string.Empty; // Workout, Meal, Steps
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public ActivityStatus Status { get; set; } = ActivityStatus.Planned;
        public int? DurationMinutes { get; set; }
        public int? Calories { get; set; }
        public int? StepsCount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public User? User { get; set; }
    }
}