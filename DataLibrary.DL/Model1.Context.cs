﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ShivFactoryEntities : DbContext
    {
        public ShivFactoryEntities()
            : base("name=ShivFactoryEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<DynamicMenu> DynamicMenus { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<DimensionMaster> DimensionMasters { get; set; }
        public virtual DbSet<WeightMaster> WeightMasters { get; set; }
        public virtual DbSet<ColorMaster> ColorMasters { get; set; }
        public virtual DbSet<BankerMaster> BankerMasters { get; set; }
        public virtual DbSet<MiniCategory> MiniCategories { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<VendorBankDetail> VendorBankDetails { get; set; }
        public virtual DbSet<Varient> Varients { get; set; }
        public virtual DbSet<ProductVarient> ProductVarients { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<TempOrder> TempOrders { get; set; }
    }
}
