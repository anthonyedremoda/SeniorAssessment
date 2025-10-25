using System.ComponentModel.DataAnnotations;

namespace SeniorEventBooking.Models
{
     public class BookingFormViewModel
    {
        [Required, StringLength(200)]
        public string Name { get; set; } = default!;

        [Required, EmailAddress, StringLength(320)]
        public string Email { get; set; } = default!;

        [StringLength(2000)]
        public string? Note { get; set; }

        public string? EventUdi { get; set; }
    }

}
