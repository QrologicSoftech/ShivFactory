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
    
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public Nullable<int> OrderId { get; set; }
        public string ProductName { get; set; }
        public string VarientName1 { get; set; }
        public string VarientValue1 { get; set; }
        public string VarientName2 { get; set; }
        public string VarientValue2 { get; set; }
        public string VarientName3 { get; set; }
        public string VarientValue3 { get; set; }
        public string VarientName4 { get; set; }
        public string VarientValue4 { get; set; }
        public string VarientName5 { get; set; }
        public string VarientValue5 { get; set; }
        public string VarientName6 { get; set; }
        public string VarientValue6 { get; set; }
        public string VarientName7 { get; set; }
        public string VarientValue7 { get; set; }
        public string VarientName8 { get; set; }
        public string VarientValue8 { get; set; }
        public string VarientName9 { get; set; }
        public string VarientValue9 { get; set; }
        public string VarientName10 { get; set; }
        public string VarientValue10 { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<decimal> ShippingCharge { get; set; }
        public Nullable<decimal> NetAmount { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
    
        public virtual Order Order { get; set; }
    }
}
