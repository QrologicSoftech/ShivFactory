using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShivFactory.Business.Repository
{
   public class SelectMember
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        public IEnumerable<MemberDetails> GetallMemberlist(int pageIndex, int pageSize)
        {
            var para = new DynamicParameters();
            para.Add("@PageIndex", pageIndex);
            para.Add("@PageSize", pageSize);
            var employees = con.Query<MemberDetails>("GetCustomersPageWise", para, commandType: CommandType.StoredProcedure);
            return employees;
        }
    }
}
