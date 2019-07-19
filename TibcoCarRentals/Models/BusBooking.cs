using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TibcoCarRentals.Models
{
	public class BusBooking
	{
		[Key]
		public int BusBookingID { get; set; }
        [Required(ErrorMessage = "The Number of Guests field is required.")]
        [DisplayName("Number of Guests")]
        [Range(1,200)]
        public int NumOfGuest { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The Date to Book field is required.")]
        [DisplayName("Date to Book")]
        public DateTime DateToBook { get; set; }
        [MaxLength(200,ErrorMessage = "The maximum length is 200 characters.")]
        [DisplayName("Special Requirements")]
		public string SpecialRequirement { get; set; }
		public string UserID { get; set; }

		public int BusID { get; set; }
		public Bus Bus { get; set; }

        // Wont be displayed
        public bool BookingConfirmed { get; set; }
        public bool BookingCompleted { get; set; }
                                           //public List<Booking> Bookings { get; set; }
    }
}
