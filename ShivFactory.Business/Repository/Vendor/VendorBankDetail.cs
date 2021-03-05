using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
    public class VendorBankDetail
    {

		public int ID { get; set; }
		public string UserID { get; set; }

		[Required(ErrorMessage="Accounder Holder Name Required")]
		public string AccountHolderName { get; set; }

		[Required(ErrorMessage = "Must be Valid and  Account Required")]
		[RegularExpression("^d{9,18}$", ErrorMessage ="Invalid Format")]
		public string AccountNumber { get; set; }

		[Required(ErrorMessage = "Must be Valid Bank ")]
		public string BankName { get; set; }

		[Required(ErrorMessage = " IFSC Code  Required")]
		[RegularExpression("^[A - Z]{4}0[A-Z0-9]{6}$", ErrorMessage = "Invalid Format")]
		public string IFSCCode { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Branch { get; set; }
		public DateTime IsActive { get; set; }
		public DateTime IsDelete { get; set; }
		public DateTime AddUpdate { get; set; }
		public DateTime LastUpdate { get; set; }
	}
}
