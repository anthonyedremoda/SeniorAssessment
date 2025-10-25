using System;
namespace SeniorEventBooking.Models
{
      public class BookingRecord
    {
        public int Id { get; set; }
        public string EventUdi { get; set; } = default!;
        public Guid? EventKey { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? Note { get; set; }
        public int? MemberId { get; set; }
        public string? ApiStatus { get; set; }
        public string? ApiResponse { get; set; }
        public DateTime CreatedUtc { get; set; }
    }

}
