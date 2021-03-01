using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ShivFactory.Business.Repository
{
    public class ClsBrand
    {
        public int? CategoryId { get; set; }
        public int? Id { get; set; }
        [Required(ErrorMessage = "Please enter brand name!!")]
        public string BrandName { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public HttpPostedFileBase PostedFile { get; set; }
    }
}
