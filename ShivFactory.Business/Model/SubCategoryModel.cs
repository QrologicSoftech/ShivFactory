using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShivFactory.Business.Model
{
    public class SubCategoryModel
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        //public string CatName { get; set; }
        public string SubCatName { get; set; }
        public string SubCatImage { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? Adddate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public HttpPostedFileBase PostedFile { get; set; }
        public IEnumerable<SelectListItem> CategoryName { get; set; }

    }
}
