namespace HealthTrackAPI.DTOs
{
    public class ActivityResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
        public int? DurationMinutes { get; set; }
        public int? Calories { get; set; }
        public int? StepsCount { get; set; }
        public string? MealType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}