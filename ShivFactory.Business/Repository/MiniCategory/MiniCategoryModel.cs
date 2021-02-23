using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ShivFactory.Business.Repository
{
    public class MiniCategoryModel
    {
        [Required(ErrorMessage = "Please select Sub category")]
        public int SubCategoryId { get; set; }
        public int MiniCategoryId { get; set; }
        [Required(ErrorMessage = "Please enter Mini Category")]
        public string MiniCategoryName { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public HttpPostedFileBase PostedFile { get; set; }

    }
}
