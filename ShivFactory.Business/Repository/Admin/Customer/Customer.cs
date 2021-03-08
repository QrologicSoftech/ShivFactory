using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository.Admin
{
 public class Customer
    {
        // made common model for adding vendor/customer through admin
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
          ErrorMessage = "Invalid format.")]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email Required!")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "FirstName Required!")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Gender Required!")]
        public string Gender { get; set; }
        public string Address { get; set; }

        public System.Web.HttpPostedFileBase PostedFile { get; set;  }
        public string UserImage { get; set; }

        // Vendor 

        public int ID { get; set; }
        public string UserID { get; set; }
        [Required(ErrorMessage = "Firm Name required")]
        public string FirmName { get; set; }
        [RegularExpression("^[0-9a-zA-Z \b]+$", ErrorMessage = "Invalid Format")]
        public string GSTIN { get; set; }
        public string FullAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        [Required(ErrorMessage = "PAN Number Required")]
        [RegularExpression("[A-Z]{5}[0-9]{4}[A-Z]{1}", ErrorMessage = "Invalid Format")]
        public string PanNo { get; set; }
        public string AddressProofImg { get; set; }
        public string PIN { get; set; }
        public string Signature { get; set; }
        public string Logo { get; set; }
        public List<System.Web.HttpPostedFileBase> files { get; set; }
        // vendor bank details 

        public int vendorbank_ID { get; set; }
        public int vendorbank_UserID { get; set; }
        [Required(ErrorMessage = "Accounder Holder Name Required")]
        public string AccountHolderName { get; set; }
        [Required(ErrorMessage = "Must be Valid  Account Required")]
        [RegularExpression("^d{9,18}$", ErrorMessage = "Invalid Format")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Must be Valid Bank ")]
        public string BankName { get; set; }

        [Required(ErrorMessage = " IFSC Code  Required")]
        [RegularExpression("^[A - Z]{4}0[A-Z0-9]{6}$", ErrorMessage = "Invalid Format")]
        public string IFSCCode { get; set; }
        public string Branch { get; set; }
    }
}
