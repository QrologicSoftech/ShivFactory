using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository.Vendor
{
   public  class Vendor
    {
		public int ID { get; set; }
		
		public int UserID { get; set; }
		[Required(ErrorMessage = "Firm Name required")]
		public string FirmName { get; set; }

		
		[RegularExpression("^[0-9a-zA-Z \b]+$", ErrorMessage = "Invalid Format")]
		public string GSTIN { get; set; }
		public string FullAddress { get; set; }
		public string City { get; set; }
		public string State { get; set; }

		[Required(ErrorMessage ="PAN Number Required"]
		[RegularExpression("[A-Z]{5}[0-9]{4}[A-Z]{1}",ErrorMessage ="Invalid Format")]
		public string PanNo { get; set; }
		public string AddressProofImg { get; set; }
		public DateTime IsActive { get; set; }
		public DateTime IsDelete { get; set; }
		public DateTime AddUpdate { get; set; }
		public DateTime LastUpdate { get; set; }
		public string PIN { get; set; }
		public string Signature { get; set; }

		public string Logo { get; set; }
	}
}
