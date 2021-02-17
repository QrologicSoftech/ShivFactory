using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ShivFactory.Business.Model
{
    public class BrandModel
    {
        public int ID { get; set; }
        public string BrandImage { get; set; }
        public string BrandName { get; set; }
        public HttpPostedFileBase PostedFile { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Adddate { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
