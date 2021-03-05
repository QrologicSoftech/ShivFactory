﻿using System;
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
		public string AddressProofImg { get; set; }
		public DateTime IsVendorActive { get; set; }
		public DateTime AddUpdate { get; set; }
		public DateTime LastUpdateVendor { get; set; }
		public string PIN { get; set; }
		public string Signature { get; set; }

		public string Logo { get; set; }
	}
}
