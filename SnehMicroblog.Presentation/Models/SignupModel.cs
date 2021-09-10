using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SnehMicroblog.Presentation.Models
{
    public class SignupModel
    {
        [Required]
        public string FirstName { get; set;  }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Invalid Password Format")]
        [StringLength(18 , ErrorMessage = "The Password Must Be at least 8 characters long" ,MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [EmailAddress]
        public string Image { get; set; }

        [EmailAddress]
        public string Country { get; set; }
    }
}