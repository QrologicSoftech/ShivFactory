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
    
    public partial class SubCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubCategory()
        {
            this.MiniCategories = new HashSet<MiniCategory>();
            this.Products = new HashSet<Product>();
        }
    
        public int ID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public string ImagePath { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> Adddate { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }
        public Nullable<decimal> GST { get; set; }
        public Nullable<decimal> SGST { get; set; }
        public Nullable<decimal> IGST { get; set; }
        public string Varients { get; set; }
        public Nullable<bool> ShowAtHome { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MiniCategory> MiniCategories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
