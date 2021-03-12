using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
   public class clsProductVarient
    {
        public int ProductId { get; set;  }
        public int ProductQty { get; set;  }
        public decimal SalePrice { get; set;  }
        public decimal ListPrice { get; set; }
        public int? SubCategoryId { get; set; }
        public string image1 { get; set;  }
        public string image2 { get; set; }
        public string image3 { get; set; }
        public string image4 { get; set; }
        public string image5 { get; set; }
        public int? varientId { get; set; }
        public string selectedVarient { get; set;}

    }
}
