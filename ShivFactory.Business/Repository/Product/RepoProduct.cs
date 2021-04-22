using DataLibrary.DL;
using ShivFactory.Business.Model;
using ShivFactory.Business.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
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


        #region Add Or Update Product new
        public bool AddOrUpdateProduct(Product model)
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
                Product.MgfDate = model.MgfDate != null ? Convert.ToDateTime(model.MgfDate) : Product.MgfDate;
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

                //  Product.BrandId = model.BrandId;
                Product.CategoryId = model.CategoryId;
                Product.SubCategoryId = model.SubCategoryId;
                Product.MiniCategoryId = model.MiniCategoryId;
                Product.IsActive = model.IsActive;
                //Product.ProductLength = model.ProductLength;
                //Product.ProductWidth = model.ProductWidth;
                //Product.ProductHeight = model.ProductHeight;
                //Product.ProductWeight = model.ProductWeight;
                //Product.PackageLength = model.PackageLength;
                //Product.PackageWidth = model.PackageWidth;
                //Product.PackageHeight = model.PackageHeight;
                //Product.PackageWeight = model.PackageWeight;
                //Product.ProductColors = model.ProductColors;
                Product.ApprovedByAdmin = null;
                Product.IsReturnable = model.IsReturnable;
                Product.ReturnDays = model.ReturnDays;
                Product.LastUpdate = DateTime.Now;
                Product.paymentModeCash = model.paymentModeCash;

            }
            else
            {
                db.Products.Add(model);
            }
            return db.SaveChanges() > 0;
        }

        #endregion

        #region GetAllProduct
        public List<DataLibrary.DL.Product> GetAllProduct(int? vendorId)
        {
            return db.Products.Where(a => a.VendorId == vendorId).AsNoTracking().ToList();
        }
        #endregion

        #region Get Product By ProductId 
        public ClsProduct1 GetProductByProductIdNew(int ProductId)
        {
            RepoCommon repoCommon = new RepoCommon();
            var Product = db.Products.Where(x => x.ProductId == ProductId).Select(a => new ClsProduct1()
            {
                ProductId = a.ProductId,
                VendorId = a.VendorId,
                ProductName = a.ProductName,
                SalePrice = a.SalePrice ?? 0,
                ListPrice = a.ListPrice ?? 0,
                LocalShipingCharge = a.LocalShipingCharge ?? 0,
                ZonalShipingCharge = a.ZonalShipingCharge ?? 0,
                NationalShippingCharge = a.NationalShippingCharge ?? 0,
                StockCount = a.StockCount ?? 0,
                MgfDate = a.MgfDate.ToString(),
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
                CategoryId = a.CategoryId,
                SubCategoryId = a.SubCategoryId,
                MiniCategoryId = a.MiniCategoryId,
                IsActive = a.IsActive ?? false,
                // ProductColors = a.ProductColors,
                IsReturnable = a.IsReturnable ?? false,
                ReturnDays = a.ReturnDays,
                ProductCode = a.productCode

            }).AsNoTracking().FirstOrDefault();
            if (Product != null && !string.IsNullOrEmpty(Product.MgfDate))
            {
                Product.MgfDate = Product.MgfDate != null ? Convert.ToDateTime(Product.MgfDate).ToString("yyyy-MM-dd") : "";
            }
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

        #region GetAllProductsByVendorId
        public List<ProductResponse> GetAllProductsByVendorId(int vendorId, PaginationRequest model, out int totalRecords)
        {
            var products = new List<ProductResponse>();
            totalRecords = 0;
            RepoCommon common = new RepoCommon();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Action", "GetAllProducts"));
            parameters.Add(new SqlParameter("@SearchText", model.searchText));
            parameters.Add(new SqlParameter("@Skip", model.Skip));
            parameters.Add(new SqlParameter("@Take", model.PageSize));
            parameters.Add(new SqlParameter("@OrderColumn", model.SortColumn));
            parameters.Add(new SqlParameter("@OrderDir", model.SortDirection));
            parameters.Add(new SqlParameter("@VendorId", vendorId));

            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnectionString, CommandType.StoredProcedure, "ManageProduct", parameters.ToArray());
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                totalRecords = ds.Tables[0].Rows[0]["TotalRow"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["TotalRow"].ToString()) : 0;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    products.Add(new ProductResponse()
                    {
                        SrNo = row["SrNo"] != DBNull.Value ? Convert.ToInt32(row["SrNo"]) : 0,
                        Id = row["ProductId"] != DBNull.Value ? Convert.ToInt32(row["ProductId"]) : 0,
                        ProductName = row["ProductName"] != DBNull.Value ? row["ProductName"].ToString() : "",
                        IsActive = row["IsActive"] != DBNull.Value ? Convert.ToBoolean(row["IsActive"]) : false,
                        AddDate = row["AddDate"] != DBNull.Value ? Convert.ToDateTime(row["AddDate"]).ToString("dd/MM/yyyy") : "",
                        ApprovedByAdmin = row["ApprovedByAdmin"] != DBNull.Value ? Convert.ToBoolean(row["ApprovedByAdmin"]) : false,
                        InactiveReason = row["InactiveReason"] != DBNull.Value ? row["InactiveReason"].ToString() : ""
                    });
                }

            }

            return products;
        }
        #endregion        

        #region Get Product color By ProductId
        public string GetProductColorByProductId(int ProductId)
        {
            //var ProductColor = db.Products.Where(x => x.ProductId == ProductId).Select(p => p.ProductColors).AsNoTracking().FirstOrDefault();
            return "";

        }
        #endregion

        #region Update Product color By ProductId
        public bool UpdateProductColorByProductId(int ProductId, string colors)
        {
            var Product = db.Products.Where(x => x.ProductId == ProductId).FirstOrDefault();
            if (Product != null)
            {
                //Product.ProductColors = colors;
                return db.SaveChanges() > 0;
            }
            return false;

        }
        #endregion

        #region Check Product Code Exist or not
        public bool IsExistProductCode(string productCode, int vendorId)
        {
            var product = db.Products.Where(a => a.productCode.ToLower() == productCode.ToLower() && a.VendorId == vendorId).AsNoTracking().FirstOrDefault();
            if (product != null)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Add Or Update Product Varient
        public bool AddOrUpdateProductVarient(ProductVarient model)
        {
            //var ProductVarients = db.ProductVarients.Where(a => a.ProductVarientId == model.ProductId).FirstOrDefault();
            //if (ProductVarients != null)
            //{// no edition on product varient 
            //}
            //else
            //{
            db.ProductVarients.Add(model);
            //}
            return db.SaveChanges() > 0;
        }

        #endregion

        #region
        public int GetProductStock(int? productvarientID)
        {
            var  stock = db.ProductVarients.Where(row => row.ProductVarientId == productvarientID).Select(row => row.Stock==null ?0:row.Stock);
            int v = Convert.ToInt32(stock);
            return v; 
        }
        #endregion


    }
}

