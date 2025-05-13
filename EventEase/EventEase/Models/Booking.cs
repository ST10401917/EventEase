using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEase.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        [ForeignKey("Venue")]
        public int VenueId { get; set; }

        [Required]
        [ForeignKey("Event")]
        public int EventId { get; set; }

        public DateTime BookingDate { get; set; }

        public Venue Venue { get; set; }
        public Event Event { get; set; }
    }
}
