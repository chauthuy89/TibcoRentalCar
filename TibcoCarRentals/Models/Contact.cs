using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TibcoCarRentals.Models
{
    public class Contact
    {
        [Key]
        public int ContactID { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        [MaxLength(100, ErrorMessage = "The maximum length is 100 characters.")]
        [MinLength(3, ErrorMessage = "The minimum length is 5 characters.")]
        public string Name { get; set; }
        [DisplayName("Phone Number")]
        public int Phone { get; set; }
        [Required(ErrorMessage = "The Email field is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "The Message field is required.")]
        [MaxLength(500, ErrorMessage = "The maximum length is 500 characters.")]
        public string Message { get; set; }
    }
}
