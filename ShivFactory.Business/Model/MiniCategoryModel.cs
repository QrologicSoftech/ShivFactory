using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShivFactory.Business.Model
{
    public class MiniCategoryModel
    {
        public int ID { get; set; }
        public int SubCatID { get; set; }
        public string MiniCatName { get; set; }
        public string MiniCatImage { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Adddate { get; set; }
        public DateTime LastUpdate { get; set; }
        public HttpPostedFileBase PostedFile { get; set; }
        public IEnumerable<SelectListItem> CategoryName { get; set; }
    }
}
