using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
  public class ProductListingPagination
    {
        public int? CategoryId { get; set;  }
        public int? SubCategoryId { get; set;  }
        public int? MiniCategoryId { get; set; }
        public string searchtext { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    
    }
}
