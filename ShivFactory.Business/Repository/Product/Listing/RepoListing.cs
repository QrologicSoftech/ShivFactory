using Dapper;
using ShivFactory.Business.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
    public class RepoListing
    {
        SqlConnection con = new SqlConnection(Connection.ConnectionString);
        #region GetAllProductList
        public List<ClsProduct> GetallProductlist(ProductListingPagination model)
        {
            List<ClsProduct> product = new List<ClsProduct>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PageIndex", model.PageIndex));
            parameters.Add(new SqlParameter("@PageSize", model.PageSize));
            parameters.Add(new SqlParameter("@CategoryId", model.CategoryId));
            parameters.Add(new SqlParameter("@SubCategoryId", model.SubCategoryId));
            parameters.Add(new SqlParameter("@MiniCategoryId", model.MiniCategoryId));
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnectionString, "GetProductsPageWise", parameters.ToArray());
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                RepoCommon repoCommon = new RepoCommon(); 
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    product.Add(new ClsProduct()
                    {
                        SrNo = row["SrNo"] != DBNull.Value ? Convert.ToInt32(row["SrNo"]) : 0,
                        ProductId = row["ProductId"] != DBNull.Value ? Convert.ToInt32(row["ProductId"]) : 0,
                        ProductName = row["ProductName"] != DBNull.Value ? row["ProductName"].ToString() : "",
                       // CategoryName = row["CategoryName"] != DBNull.Value ? row["CategoryName"].ToString() : "",
                      //  SubCategoryName = row["SubCategoryName"] != DBNull.Value ? row["SubCategoryName"].ToString() : "",
                        SalePrice = row["SalePrice"] != DBNull.Value ? row["SalePrice"].ToString() : "0.00",
                        ListPrice = row["ListPrice"] != DBNull.Value ? row["ListPrice"].ToString() : "0.00",
                        MainImage = repoCommon.checkfile(row["MainImage"].ToString())
                    });
                }
               
            }
            return product;
        }
        #endregion


        #region GetProductDetail
        public ProductDetail GetProductDetail(int productId,string VarientName,string VarientValue)
        {
            //foreach (DataRow row in ds.Tables[0].Rows)
            //{
            RepoCommon repoCommon = new RepoCommon();
              var productDetail = new ProductDetail();
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@ProductId", productId));
            param.Add(new SqlParameter("@VarientName", VarientName));
            param.Add(new SqlParameter("@VarientValue", VarientValue));
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnectionString, CommandType.StoredProcedure, "GetProductDetail",param.ToArray());
            if (ds.Tables[0].Rows.Count > 0)
            {
                productDetail = new ProductDetail()
                    {
                    ProductId = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductId"]),
                    ProductVarientId = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductVarientId"]),
                    ProductName = Convert.ToString(ds.Tables[0].Rows[0]["ProductName"]),
                    Description = ds.Tables[0].Rows[0]["Description"]!=DBNull.Value? ds.Tables[0].Rows[0]["Description"].ToString():"",
                    SalePrice = ds.Tables[0].Rows[0]["SalePrice"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["SalePrice"]) : 0,
                    ListPrice = ds.Tables[0].Rows[0]["ListPrice"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["ListPrice"]) : 0,
                    productWarning = ds.Tables[0].Rows[0]["productWarning"] != DBNull.Value ? ds.Tables[0].Rows[0]["productWarning"].ToString() : "",
                    ReturnDays = ds.Tables[0].Rows[0]["ReturnDays"] != DBNull.Value ? Convert.ToInt16(ds.Tables[0].Rows[0]["ReturnDays"]) : 0,
                    MainImage = repoCommon.checkfile(ds.Tables[0].Rows[0]["MainImage"].ToString()),
                    Image1 = repoCommon.checkfile(ds.Tables[0].Rows[0]["Image1"].ToString()),
                    Image2 = repoCommon.checkfile(ds.Tables[0].Rows[0]["Image2"].ToString()),
                    Image3 = repoCommon.checkfile(ds.Tables[0].Rows[0]["Image3"].ToString()),
                    Image4 = repoCommon.checkfile(ds.Tables[0].Rows[0]["Image4"].ToString()),
                    Image5 = repoCommon.checkfile(ds.Tables[0].Rows[0]["Image5"].ToString()),
                };
            }
            if (ds.Tables[1].Rows.Count > 0)
            { 
            
            }
            return productDetail;
        }
        #endregion
    }
}

            
           