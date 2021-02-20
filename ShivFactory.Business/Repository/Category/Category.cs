using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ShivFactory.Business.Repository
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Please enter category name")]
        public string CategoryName { get; set; }
        public string ImagePath { get; set; }
        public bool? IsActive { get; set; }
        //[Required(ErrorMessage = "Please upload image")]
        public HttpPostedFileBase PostedFile { get; set; }
    }
}
