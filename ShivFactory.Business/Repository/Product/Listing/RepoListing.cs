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
        
        public IEnumerable<ClsProduct> GetallProductlist(int pageIndex, int pageSize)
        {
            var para = new DynamicParameters();
            para.Add("@PageIndex", pageIndex);
            para.Add("@PageSize", pageSize);
            //para.Add("@CategoryId", null);
            //para.Add("@SubCategoryId", null);
            var products = con.Query<ClsProduct>("GetCustomersPageWise", para, commandType: CommandType.StoredProcedure);
            return products;
        }
    }
}
