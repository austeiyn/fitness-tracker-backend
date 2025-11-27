namespace HealthTrackAPI.DTOs
{
    public class RegisterResponse
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Message { get; set; } = "Registration successful!";
        public DateTime CreatedAt { get; set; }
    }
}
