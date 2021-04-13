//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLibrary.DL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vendor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vendor()
        {
            this.Products = new HashSet<Product>();
            this.TempOrderDetails = new HashSet<TempOrderDetail>();
            this.VendorBankDetails = new HashSet<VendorBankDetail>();
            this.VendorShippingAreas = new HashSet<VendorShippingArea>();
        }
    
        public int VendorId { get; set; }
        public string UserId { get; set; }
        public string FirmName { get; set; }
        public string GSTIN { get; set; }
        public string FullAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PanNo { get; set; }
        public string AddressProofImg { get; set; }
        public Nullable<System.DateTime> AddUpdate { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }
        public string PIN { get; set; }
        public string Signature { get; set; }
        public string Logo { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TempOrderDetail> TempOrderDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VendorBankDetail> VendorBankDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VendorShippingArea> VendorShippingAreas { get; set; }
    }
}
