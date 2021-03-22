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

        public List<ClsProduct> GetallProductlist(ProductListingPagination model,int pageIndex, int pageSize)
        {
            List<ClsProduct> product = new List<ClsProduct>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PageIndex", pageIndex));
            parameters.Add(new SqlParameter("@PageSize", pageSize));
            parameters.Add(new SqlParameter("@CategoryId", pageSize));
            parameters.Add(new SqlParameter("@SubCategoryId", pageSize));
            parameters.Add(new SqlParameter("@MiniCategoryId", pageSize));
            parameters.Add(new SqlParameter("@SearchText", pageSize));
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnectionString, "GetProductsPageWise", parameters.ToArray());
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    product.Add(new ClsProduct()
                    {
                        SrNo = row["SrNo"] != DBNull.Value ? Convert.ToInt32(row["SrNo"]) : 0,
                        ProductId = row["ProductId"] != DBNull.Value ? Convert.ToInt32(row["ProductId"]) : 0,
                        ProductName = row["ProductName"] != DBNull.Value ? row["ProductName"].ToString() : "",
                        CategoryName = row["CategoryName"] != DBNull.Value ? row["CategoryName"].ToString() : "",
                        SubCategoryName = row["SubCategoryName"] != DBNull.Value ? row["SubCategoryName"].ToString() : "",
                        BrandName = row["BrandName"] != DBNull.Value ? row["BrandName"].ToString() : "",
                        PageCount = row["PageCount"] != DBNull.Value ? Convert.ToInt32(row["PageCount"].ToString()) : 0
                    });
                }
               
            }
            return product;
        }
    }
}

            
           