using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
    
    public class UserProfile
    {
		public int UserDetailId { get; set; }
		public string UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Gender { get; set; }
		public string Email { get; set; }
		public string Mobile { get; set; }
		public string Password { get; set; }
		public string UserImage { get; set; }
		public string Address { get; set; }
		public string LastLoginIP { get; set; }
		public DateTime LastLoginDate { get; set; }
		public DateTime AddDate { get; set; }
		public DateTime LastUpdate { get; set; }
		public bool IsActive { get; set; }
		public bool IsDelete { get; set; }

		// Vendor Details
		public string FirmName { get; set; }

		public string GSTIN { get; set; }
		public string FullAddress { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string PanNo { get; set; }
		public string AddressProofImg { get; set; }
		public DateTime IsVendorActive { get; set; }
		public DateTime AddUpdate { get; set; }
		public DateTime LastUpdateVendor { get; set; }
		public string PIN { get; set; }
		public string Signature { get; set; }

		public string Logo { get; set; }

		//Vendor Bank Details
		public string AccountHolderName { get; set; }
		public string AccountNumber { get; set; }
		public string BankName { get; set; }
		public string IFSCCode { get; set; }
		public string Branch { get; set; }



	}
}
