using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
    public class SubCategoryResponse
    {
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public string Adddate { get; set; }
    }
}
