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
        public List<UnApprovedProductResponse> GetAllUnApprovedProducts(PaginationRequest model, out int totalRecords)
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
                        BrandName = row["BrandName"] != DBNull.Value ? row["BrandName"].ToString() : "",
                        AddDate = row["AddDate"] != DBNull.Value ? Convert.ToDateTime(row["AddDate"]).ToString("dd/MM/yyyy") : "",
                        InactiveReason = row["InactiveReason"] != DBNull.Value ? row["InactiveReason"].ToString() : ""
                    });
                }

            }

            return products;
        }
        #endregion

        #region Get Product Images By ProductId
        public List<string> GetProductImagesByProductId(int ProductId)
        {
            var images = new List<string>();
            RepoCommon repoCommon = new RepoCommon();
            var Product = db.Products.Where(x => x.ProductId == ProductId).AsNoTracking().FirstOrDefault();
            if (Product != null)
            {
                images.Add(repoCommon.checkfile(Product.MainImage));
                images.Add(repoCommon.checkfile(Product.Image1));
                images.Add(repoCommon.checkfile(Product.Image2));
                images.Add(repoCommon.checkfile(Product.Image3));
                images.Add(repoCommon.checkfile(Product.Image4));
                images.Add(repoCommon.checkfile(Product.Image5));
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
                ProductColors = a.ProductColors
            }).AsNoTracking().FirstOrDefault();
            if (productInfo != null && !string.IsNullOrEmpty(productInfo.MgfDate))
            {
                productInfo.MgfDate = Convert.ToDateTime(productInfo.MgfDate).ToString("dd/MM/yyyy");
            }
            return productInfo;

        }
        #endregion

        #region Get Product Dimension By ProductId
        public ProductDimensionResponse GetProductDimensionByProductId(int ProductId)
        {
            var productDimension = db.Products.Where(x => x.ProductId == ProductId).Select(a => new ProductDimensionResponse()
            {
                ProductLength = a.ProductLength,
                ProductWidth = a.ProductWidth,
                ProductHeight = a.ProductHeight,
                ProductWeight = a.ProductWeight,
                PackageLength = a.PackageLength,
                PackageWidth = a.PackageWidth,
                PackageHeight = a.PackageHeight,
                PackageWeight = a.PackageWeight
            }).AsNoTracking().FirstOrDefault();

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
    }
}
