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
   public  class RepoListing
    {
        SqlConnection  con = new SqlConnection(Connection.ConnectionString);

        public List<ClsProduct> GetallProductlist(int pageIndex, int pageSize)
        {
            List<ClsProduct> product = new List<ClsProduct>();

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PageIndex", pageIndex));
            parameters.Add(new SqlParameter("@PageSize", pageSize));
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnectionString, "GetProductsPageWise", parameters.ToArray());
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            { 
            
            }
            return product;
        }
    }
}
