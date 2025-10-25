namespace SeniorEventBooking.Models
{
    public class EventBookings
    {
        public int Id { get; set; }
        public string EventUdi { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
