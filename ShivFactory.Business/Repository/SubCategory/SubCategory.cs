using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ShivFactory.Business.Repository
{
    public class SubCategoryModel
    {
        [Required(ErrorMessage = "Please select category")]
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        [Required(ErrorMessage = "Please enter subCategory name")]
        public string SubCategoryName { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public HttpPostedFileBase PostedFile { get; set; }
    }
}
