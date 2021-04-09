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

namespace ShivFactory.Business.Repository.Admin
{
    public class RepoProductDetails
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        #endregion

        #region GetAllUnApprovedProducts
        public List<UnApprovedProductResponse> GetAllUnApprovedProducts(ProductModel model, out int totalRecords)
        {
            var products = new List<UnApprovedProductResponse>();
            totalRecords = 0;
            RepoCommon common = new RepoCommon();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Action", "GetAllUnApprovedProducts"));
            parameters.Add(new SqlParameter("@SearchText", model.searchText));
            parameters.Add(new SqlParameter("@Skip", model.Skip));
            parameters.Add(new SqlParameter("@Take", model.PageSize));
            parameters.Add(new SqlParameter("@OrderColumn", model.SortColumn));
            parameters.Add(new SqlParameter("@OrderDir", model.SortDirection));
            parameters.Add(new SqlParameter("@VendorId", 0));
            parameters.Add(new SqlParameter("@ApprovedByAdmin", model.ApprovedStatus));

            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnectionString, "ManageProduct", parameters.ToArray());
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                totalRecords = ds.Tables[0].Rows[0]["TotalRow"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["TotalRow"].ToString()) : 0;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    products.Add(new UnApprovedProductResponse()
                    {
                        SrNo = row["SrNo"] != DBNull.Value ? Convert.ToInt32(row["SrNo"]) : 0,
                        Id = row["ProductId"] != DBNull.Value ? Convert.ToInt32(row["ProductId"]) : 0,
                        ProductName = row["ProductName"] != DBNull.Value ? row["ProductName"].ToString() : "",
                        CategoryName = row["CategoryName"] != DBNull.Value ? row["CategoryName"].ToString() : "",
                        SubCategoryName = row["SubCategoryName"] != DBNull.Value ? row["SubCategoryName"].ToString() : "",
                        AddDate = row["AddDate"] != DBNull.Value ? Convert.ToDateTime(row["AddDate"]).ToString("dd/MM/yyyy") : "",
                        InactiveReason = row["InactiveReason"] != DBNull.Value ? row["InactiveReason"].ToString() : ""
                    });
                }

            }

            return products;
        }
        #endregion

        #region Get Product Images By ProductId
        public List<string> GetProductImagesByProductId(int ProductId, int varientId)
        {
            var images = new List<string>();
            RepoCommon repoCommon = new RepoCommon();
            var Product = db.Products.Where(x => x.ProductId == ProductId).AsNoTracking().FirstOrDefault();
            if (Product != null)
            {
                images.Add(Product.MainImage.ImagePath());
                images.Add(Product.Image1.ImagePath());
                images.Add(Product.Image2.ImagePath());
                images.Add(Product.Image3.ImagePath());
                images.Add(Product.Image4.ImagePath());
                images.Add(Product.Image5.ImagePath());
            }
            else
            {
                var varient = db.ProductVarients.Where(x => x.ProductVarientId == varientId).AsNoTracking().FirstOrDefault();
                if (varient != null)
                {
                    images.Add(varient.MainImage.ImagePath());
                    images.Add(varient.Image1.ImagePath());
                    images.Add(varient.Image2.ImagePath());
                    images.Add(varient.Image3.ImagePath());
                    images.Add(varient.Image4.ImagePath());
                    images.Add(varient.Image5.ImagePath());
                }
            }
            return images;

        }
        #endregion

        #region Get Product BasicInfo By ProductId
        public ProductBasicInfoResponse GetProductBasicInfoByProductId(int ProductId)
        {
            var productInfo = db.Products.Where(x => x.ProductId == ProductId).Include(a => a.Vendor).Select(a => new ProductBasicInfoResponse()
            {
                VendorName = a.Vendor != null ? a.Vendor.FirmName : "",
                SalePrice = a.SalePrice ?? 0,
                ListPrice = a.ListPrice ?? 0,
                LocalShipingCharge = a.LocalShipingCharge ?? 0,
                ZonalShipingCharge = a.ZonalShipingCharge ?? 0,
                NationalShippingCharge = a.NationalShippingCharge ?? 0,
                EstimateDeliveryTime = a.EstimateDeliveryTime,
                IsActive = a.IsActive ?? false,
                IsReturnable = a.IsReturnable ?? false,
                ReturnDays = a.ReturnDays
            }).AsNoTracking().FirstOrDefault();

            return productInfo;

        }
        #endregion

        #region Get Product Detail By ProductId
        public ProductDetailResponse GetProductDetailByProductId(int ProductId)
        {
            var productInfo = db.Products.Where(x => x.ProductId == ProductId).Select(a => new ProductDetailResponse()
            {
                MgfDate = a.MgfDate.ToString(),
                MgfDetail = a.MgfDetail,
                ShellLife = a.ShellLife,
                StockCount = a.StockCount ?? 0,
                ProductWarning = a.ProductWarning,
                Description = a.Description,
                //ProductColors = a.ProductColors
            }).AsNoTracking().FirstOrDefault();
            if (productInfo != null && !string.IsNullOrEmpty(productInfo.MgfDate))
            {
                productInfo.MgfDate = Convert.ToDateTime(productInfo.MgfDate).ToString("dd/MM/yyyy");
            }
            return productInfo;

        }
        #endregion

        #region Get Product Varients By ProductId
        public List<ProductVarientResponse> GetProductVarientsByProductId(int ProductId)
        {
            var productDimension = db.ProductVarients.Where(x => x.ProductId == ProductId).Select(a => new ProductVarientResponse()
            {
                Id = a.ProductVarientId,
                ProductQty = a.Stock ?? 0,
                SalePrice = a.SalePrice ?? 0,
                ListPrice = a.ListPrice ?? 0,
                Feature1 = a.VarientName1 != null && a.VarientValue1 != null ? a.VarientName1 + ":" + a.VarientValue1 : a.VarientName1 + a.VarientValue1,
                Feature2 = a.VarientName2 != null && a.VarientValue2 != null ? a.VarientName2 + ":" + a.VarientValue2 : a.VarientName2 + a.VarientValue2,
                Feature3 = a.VarientName3 != null && a.VarientValue3 != null ? a.VarientName3 + ":" + a.VarientValue3 : a.VarientName3 + a.VarientValue3,
                Feature4 = a.VarientName4 != null && a.VarientValue4 != null ? a.VarientName4 + ":" + a.VarientValue4 : a.VarientName4 + a.VarientValue4,
                Feature5 = a.VarientName5 != null && a.VarientValue5 != null ? a.VarientName5 + ":" + a.VarientValue5 : a.VarientName5 + a.VarientValue5,
                Feature6 = a.VarientName6 != null && a.VarientValue6 != null ? a.VarientName6 + ":" + a.VarientValue6 : a.VarientName6 + a.VarientValue6,
                Feature7 = a.VarientName7 != null && a.VarientValue7 != null ? a.VarientName7 + ":" + a.VarientValue7 : a.VarientName7 + a.VarientValue7,
                Feature8 = a.VarientName8 != null && a.VarientValue8 != null ? a.VarientName8 + ":" + a.VarientValue8 : a.VarientName8 + a.VarientValue8,
                Feature9 = a.VarientName9 != null && a.VarientValue9 != null ? a.VarientName9 + ":" + a.VarientValue9 : a.VarientName9 + a.VarientValue9,
                Feature10 = a.VarientName10 != null && a.VarientValue10 != null ? a.VarientName10 + ":" + a.VarientValue10 : a.VarientName10 + a.VarientValue10,
                Image = a.MainImage,
                Image1 = a.Image1,
                Image2 = a.Image2,
                Image3 = a.Image3,
                Image4 = a.Image4,
                Image5 = a.Image5,
            }).AsNoTracking().ToList();

            return productDimension;

        }
        #endregion

        #region Approved Product By Admin
        public bool ApprovedProductByAdmin(int ProductId)
        {
            var product = db.Products.Where(x => x.ProductId == ProductId).FirstOrDefault();
            if (product != null)
            {
                product.ApprovedByAdmin = true;
                return db.SaveChanges() > 0;
            }
            return false;
        }
        #endregion

        #region Reject Product By Admin
        public bool RejectProductByAdmin(int ProductId, string rejectRegion)
        {
            var product = db.Products.Where(x => x.ProductId == ProductId).FirstOrDefault();
            if (product != null)
            {
                product.ApprovedByAdmin = false;
                product.InactiveReason = rejectRegion;
                return db.SaveChanges() > 0;
            }
            return false;
        }
        #endregion

        #region Update Varient Quantity
        public bool UpdateVarientQuantity(int varientId, int qty)
        {
            var product = db.ProductVarients.Where(x => x.ProductVarientId == varientId).FirstOrDefault();
            if (product != null)
            {
                product.Stock = qty;
                return db.SaveChanges() > 0;
            }
            return false;
        }
        #endregion
    }
}
