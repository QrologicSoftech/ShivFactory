using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
    public class clsVendorBasicDetails
    {
		// Vendor Details
		public string FirmName { get; set; }
		public string GSTIN { get; set; }
		public string FullAddress { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string PanNo { get; set; }
		public string PIN { get; set; }

		public string AddressProof { get; set; }

	}
}
