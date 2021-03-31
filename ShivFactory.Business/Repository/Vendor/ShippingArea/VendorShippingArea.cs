using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ShivFactory.Business.Repository
{
    public class VendorShippingAreaModel
    {
        public int ID { get; set;  }
        public int VendorId { get; set; }
        public string VendorName { get; set; }

        [Required(ErrorMessage = "Enter your shipping area pincode")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Please Enter Valid Postal Code.")]
        public string Pincode { get; set; }

    }
}
