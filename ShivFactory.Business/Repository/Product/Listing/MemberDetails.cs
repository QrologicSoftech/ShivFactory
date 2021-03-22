using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
  public class MemberDetails
    {
        public Int64 CustomerID { get; set; }
        public Int64 RowNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Int32 PageCount { get; set; }
    }
}
