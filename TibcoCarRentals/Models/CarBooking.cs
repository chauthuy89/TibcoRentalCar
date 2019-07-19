using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TibcoCarRentals.Models
{
    public class CarBooking
    {
        [Key]
        public int BookingID { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The Start Date field is required.")]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "The Finish Date field is required.")]
        [DisplayName("Finish Date")]
        [DataType(DataType.Date)]
        public DateTime FinishDate { get; set; }
        // Hidden Attributes
        public bool BookingConfirmed { get; set; }
        public bool BookingCompleted { get; set; }
        public string UserID { get; set; }
		public int CarID { get; set; }
        public Car Car { get; set; }
    }
}
