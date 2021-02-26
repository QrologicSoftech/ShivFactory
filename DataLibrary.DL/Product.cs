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
    
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public Nullable<decimal> ListPrice { get; set; }
        public string MainImage { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        public string Image6 { get; set; }
        public Nullable<int> Category { get; set; }
        public Nullable<int> SubCategory { get; set; }
        public Nullable<int> MiniCategory { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> AddUpdate { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }
    
        public virtual Category Category1 { get; set; }
        public virtual MiniCategory MiniCategory1 { get; set; }
        public virtual SubCategory SubCategory1 { get; set; }
    }
}