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
        public int? SubCategoryId { get; set; }
        [Required(ErrorMessage = "Please enter subCategory name")]
        public string SubCategoryName { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public HttpPostedFileBase PostedFile { get; set; }

        [Required(ErrorMessage = "GST Required")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid GST")]
        [Range(0, int.MaxValue)]
        public decimal GST { get; set;  }
        [Required(ErrorMessage = "SGST Required")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid SGST")]
        [Range(0, int.MaxValue)]
        public decimal SGST { get; set; }
        [Required(ErrorMessage = "IGST Required")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid IGST")]
        [Range(0, int.MaxValue)]
        public decimal IGST { get; set; }
    }
}
