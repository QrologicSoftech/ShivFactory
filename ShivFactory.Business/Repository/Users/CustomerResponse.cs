using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
    public class CustomerResponse
    {
        public int SrNo { get; set; }
        public string Id { get; set; }
        public string ImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string AddDate { get; set; }
    }
}
