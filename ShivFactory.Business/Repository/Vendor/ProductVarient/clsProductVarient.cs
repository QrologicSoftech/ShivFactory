using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
   public class clsProductVarient
    {
        public int productId { get; set;  }
        public int productQty { get; set;  }
        public decimal salePrice { get; set;  }
        public decimal listPrice { get; set; }
        public int SubCatId { get; set; }
        public string image1 { get; set;  }
        public string image2 { get; set; }
        public string image3 { get; set; }
        public string image4 { get; set; }
        public string image5 { get; set; }

    }
}
