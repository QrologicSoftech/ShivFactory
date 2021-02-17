using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ShivFactory.Business.Model
{
    public class BannerModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string BannerImage { get; set; }
        public HttpPostedFileBase PostedFile { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Adddate { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
