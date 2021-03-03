using DataLibrary.DL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShivFactory.Business.Repository
{
    public class RepoProduct
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        #endregion

        #region Add Or Update Product
        public bool AddOrUpdateProduct(ClsProduct model)
        {
            var Product = db.Products.Where(a => a.ProductId == model.ProductId).FirstOrDefault();
            if (Product != null)
            {
                Product.VendorId = model.VendorId;
                Product.ProductName = model.ProductName;
                Product.SalePrice = model.SalePrice;
                Product.ListPrice = model.ListPrice;
                Product.LocalShipingCharge = model.LocalShipingCharge;
                Product.ZonalShipingCharge = model.ZonalShipingCharge;
                Product.NationalShippingCharge = model.NationalShippingCharge;
                Product.StockCount = model.StockCount;
                Product.MgfDate = model.MgfDate;
                Product.MgfDetail = model.MgfDetail;
                Product.ShellLife = model.ShellLife;
                Product.ProductWarning = model.ProductWarning;
                Product.Description = model.Description;
                Product.EstimateDeliveryTime = model.EstimateDeliveryTime;

                Product.MainImage = model.MainImage;
                Product.Image1 = model.Image1;
                Product.Image2 = model.Image2;
                Product.Image3 = model.Image3;
                Product.Image4 = model.Image4;
                Product.Image5 = model.Image5;
                Product.Image6 = model.Image6;

                Product.BrandId = model.BrandId;
                Product.CategoryId = model.CategoryId;
                Product.SubCategoryId = model.SubCategoryId;
                Product.MiniCategoryId = model.MiniCategoryId;
                Product.IsActive = model.IsActive;
                Product.ProductLength = model.ProductLength;
                Product.ProductWidth = model.ProductWidth;
                Product.ProductHeight = model.ProductHeight;
                Product.ProductWeight = model.ProductWeight;
                Product.PackageLength = model.PackageLength;
                Product.PackageWidth = model.PackageWidth;
                Product.PackageHeight = model.PackageHeight;
                Product.PackageWeight = model.PackageWeight;
                Product.ProductColors = model.ProductColors;
                Product.ApprovedByAdmin = false;
                Product.IsReturnable = model.IsReturnable;
                Product.ReturnDays = model.ReturnDays;
                Product.LastUpdate = DateTime.Now;
            }
            else
            {
                var product=new Product()
                {
                    VendorId = model.VendorId,
                    ProductName = model.ProductName,
                    SalePrice = model.SalePrice,
                    ListPrice = model.ListPrice,
                    LocalShipingCharge = model.LocalShipingCharge,
                    ZonalShipingCharge = model.ZonalShipingCharge,
                    NationalShippingCharge = model.NationalShippingCharge,
                    StockCount = model.StockCount,
                    MgfDate = model.MgfDate,
                    MgfDetail = model.MgfDetail,
                    ShellLife = model.ShellLife,
                    ProductWarning = model.ProductWarning,
                    Description = model.Description,
                    EstimateDeliveryTime = model.EstimateDeliveryTime,
                    MainImage = model.MainImage,
                    Image1 = model.Image1,
                    Image2 = model.Image2,
                    Image3 = model.Image3,
                    Image4 = model.Image4,
                    Image5 = model.Image5,
                    Image6 = model.Image6,
                    BrandId = model.BrandId,
                    CategoryId = model.CategoryId,
                    SubCategoryId = model.SubCategoryId,
                    MiniCategoryId = model.MiniCategoryId,
                    IsActive = model.IsActive,
                    ProductLength = model.ProductLength,
                    ProductWidth = model.ProductWidth,
                    ProductHeight = model.ProductHeight,
                    ProductWeight = model.ProductWeight,
                    PackageLength = model.PackageLength,
                    PackageWidth = model.PackageWidth,
                    PackageHeight = model.PackageHeight,
                    PackageWeight = model.PackageWeight,
                    ProductColors = model.ProductColors,
                    ApprovedByAdmin = false,
                    IsReturnable = model.IsReturnable,
                    ReturnDays = model.ReturnDays,
                    AddDate = DateTime.Now
                };
                db.Products.Add(new Product
                {
                    VendorId = model.VendorId,
                    ProductName = model.ProductName,
                    SalePrice = model.SalePrice,
                    ListPrice = model.ListPrice,
                    LocalShipingCharge = model.LocalShipingCharge,
                    ZonalShipingCharge = model.ZonalShipingCharge,
                    NationalShippingCharge = model.NationalShippingCharge,
                    StockCount = model.StockCount,
                    MgfDate = model.MgfDate,
                    MgfDetail = model.MgfDetail,
                    ShellLife = model.ShellLife,
                    ProductWarning = model.ProductWarning,
                    Description = model.Description,
                    EstimateDeliveryTime = model.EstimateDeliveryTime,
                    MainImage = model.MainImage,
                    Image1 = model.Image1,
                    Image2 = model.Image2,
                    Image3 = model.Image3,
                    Image4 = model.Image4,
                    Image5 = model.Image5,
                    Image6 = model.Image6,
                    BrandId = model.BrandId,
                    CategoryId = model.CategoryId,
                    SubCategoryId = model.SubCategoryId,
                    MiniCategoryId = model.MiniCategoryId,
                    IsActive = model.IsActive,
                    ProductLength = model.ProductLength,
                    ProductWidth = model.ProductWidth,
                    ProductHeight = model.ProductHeight,
                    ProductWeight = model.ProductWeight,
                    PackageLength = model.PackageLength,
                    PackageWidth = model.PackageWidth,
                    PackageHeight = model.PackageHeight,
                    PackageWeight = model.PackageWeight,
                    ProductColors = model.ProductColors,
                    ApprovedByAdmin = false,
                    IsReturnable = model.IsReturnable,
                    ReturnDays = model.ReturnDays,
                    AddDate = DateTime.Now
                });
            }
            return db.SaveChanges() > 0;
        }

        #endregion

        #region GetAllProduct
        public List<DataLibrary.DL.Product> GetAllProduct(int? vendorId)
        {
            return db.Products.Where(a=>a.VendorId==vendorId).AsNoTracking().ToList();
        }
        #endregion

        #region Get Product By ProductId
        public ClsProduct GetProductByProductId(int ProductId)
        {
            var Product = db.Products.Where(x => x.ProductId == ProductId).Select(a => new ClsProduct()
            {
                ProductId = a.ProductId,
                VendorId = a.VendorId,
                ProductName = a.ProductName,
                SalePrice = a.SalePrice.Value,
                ListPrice = a.ListPrice.Value,
                LocalShipingCharge = a.LocalShipingCharge.Value,
                ZonalShipingCharge = a.ZonalShipingCharge.Value,
                NationalShippingCharge = a.NationalShippingCharge.Value,
                StockCount = a.StockCount.Value,
                MgfDate = a.MgfDate,
                MgfDetail = a.MgfDetail,
                ShellLife = a.ShellLife,
                ProductWarning = a.ProductWarning,
                Description = a.Description,
                EstimateDeliveryTime = a.EstimateDeliveryTime,
                MainImage = a.MainImage,
                Image1 = a.Image1,
                Image2 = a.Image2,
                Image3 = a.Image3,
                Image4 = a.Image4,
                Image5 = a.Image5,
                Image6 = a.Image6,
                BrandId = a.BrandId,
                CategoryId = a.CategoryId,
                SubCategoryId = a.SubCategoryId,
                MiniCategoryId = a.MiniCategoryId,
                IsActive = a.IsActive ?? false,
                ProductLength = a.ProductLength,
                ProductWidth = a.ProductWidth,
                ProductHeight = a.ProductHeight,
                ProductWeight = a.ProductWeight,
                PackageLength = a.PackageLength,
                PackageWidth = a.PackageWidth,
                PackageHeight = a.PackageHeight,
                PackageWeight = a.PackageWeight,
                ProductColors = a.ProductColors,
                IsReturnable = a.IsReturnable ?? false,
                ReturnDays = a.ReturnDays
            }).AsNoTracking().FirstOrDefault();

            return Product;

        }
        #endregion

        #region Delete Product By ProductId
        public bool DeleteProductByProductId(int ProductId)
        {
            var Product = db.Products.Where(x => x.ProductId == ProductId).FirstOrDefault();
            if (Product != null)
            {
                db.Products.Remove(Product);
            }
            return db.SaveChanges() > 0;
        }
        #endregion
    }
}
