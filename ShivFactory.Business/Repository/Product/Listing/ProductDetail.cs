using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
    public class ProductDetail
    {
        public int ProductId { get; set; }
        public int ProductVarientId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal SalePrice { get; set; }
        public decimal ListPrice { get; set; }
        public int Stock { get; set; }
        public string productWarning { get; set; }
        public int ReturnDays { get; set; }
        public string ProductWeight { get; set; }
        public string MainImage { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        public List<ProductVariations> varientList { get; set;  }
        public string FirmName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string  MiniCategoryName { get; set; }

        public bool IsReturnable { get; set;  }
        public bool paymentModeCash { get; set;  }

        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int MiniCategoryId { get; set; }
        public int vendorId { get; set; }

        public string EstimateDeliveryTime { get; set;  }
    }

    public class ProductVariations
    {
        public string ImagePath { get; set; }
        public string VarientName { get; set;  }
        public List<string> VarientValue { get; set; }

    }
}
