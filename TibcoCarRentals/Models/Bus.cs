using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TibcoCarRentals.Models
{
    public class Bus
    {
        [Key]
        public int BusID { get; set; }
        [Range(1940,2019)]
        public int Year { get; set; }
        [Required(ErrorMessage = "The Daily Price field is required.")]
        [DisplayName("Daily Price")]
        public double DailyPrice { get; set; }
        [Required(ErrorMessage = "The Model field is required.")]
        [StringLength(50,ErrorMessage ="The Model length cannot be longer than 50 characters.")]
        public string Model { get; set; }
        [Required(ErrorMessage = "The Number of Seats field is required.")]
        [DisplayName("Number of Seats")]
        [MinLength(5,ErrorMessage = "The Number of Seats cannot be less than 5."), MaxLength(100, ErrorMessage ="The Number of Seats cannot be bigger than 100.")]
        public int NumSeats { get; set; }
		[DataType(DataType.Date)]
        public DateTime Warrent { get; set; }
		[DataType(DataType.Date)]
		public DateTime Registration { get; set; }
		public List<BusBooking> BusBookings { get; set; }
        [DisplayName("Image Location")]
        public string ImageLocation { get; set; }
        [MinLength(0, ErrorMessage = "The Quantity cannot be less than 0."), MaxLength(30, ErrorMessage = "The Quantity cannot be bigger than 30.")]
        public int Quantity { get; set; }
    }
}
