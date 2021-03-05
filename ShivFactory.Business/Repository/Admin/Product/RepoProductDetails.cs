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
    }
}
