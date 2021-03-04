using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
   public  class VendorRegister
    {
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
           ErrorMessage = "Invalid format.")]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        public string code { get; set; }
        //public string message { get; set; }

        public bool isOTPSend { get; set;  }
       
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "Email Required!")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "FirstName Required!")]
        public string FirstName { get; set; }
        public string UserId { get; set; }
        //public string LastName { get; set; }

        //public string Gender { get; set; }
        //public string Address { get; set; }
    }
}
