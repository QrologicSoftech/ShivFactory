using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
    public class ClsVarient
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Please enter varient name!!")]
        public string Varient { get; set; }
        public bool IsActive { get; set; }
    }
}
