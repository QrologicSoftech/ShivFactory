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
    
    public partial class Varient
    {
        public int Id { get; set; }
        public string VarientName { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<int> SubCategoryId { get; set; }
        public Nullable<bool> Isactive { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual SubCategory SubCategory { get; set; }
    }
}
