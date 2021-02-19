using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ShivFactory.Business.Model
{
    public class CategoryModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Please Enter Category Name.")]
        public string CategoryName { get; set; }
        //[Required(ErrorMessage = "Please select Category Image.")]
        public string CatImage { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? Adddate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public HttpPostedFileBase PostedFile { get; set; }

    }
}
