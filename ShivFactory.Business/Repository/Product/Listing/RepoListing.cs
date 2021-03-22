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
                        CategoryName = row["CategoryName"] != DBNull.Value ? row["CategoryName"].ToString() : "",
                        SubCategoryName = row["SubCategoryName"] != DBNull.Value ? row["SubCategoryName"].ToString() : "",
                        SalePrice = row["SalePrice"] != DBNull.Value ? row["SalePrice"].ToString() : "0.00",
                        ListPrice = row["ListPrice"] != DBNull.Value ? row["ListPrice"].ToString() : "0.00",
                        MainImage = repoCommon.checkfile(row["MainImage"].ToString())
                    });
                }
               
            }
            return product;
        }
    }
}

            
           