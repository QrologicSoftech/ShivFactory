using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
   public class clsProductVarient
    {
        public int ProductId { get; set; }
        public int ProductQty { get; set; }
        public decimal SalePrice { get; set; }
        public decimal ListPrice { get; set; }
        public int? SubCategoryId { get; set; }
        public string image1 { get; set; }
        public string image2 { get; set; }
        public string image3 { get; set; }
        public string image4 { get; set; }
        public string image5 { get; set; }
        public int? varientId { get; set; }
        public string selectedVarient { get; set; }
        public string VarientName1 { get; set; }
        public string VarientValue1 { get; set; }
        public string VarientName2 { get; set; }
        public string VarientValue2 { get; set; }
        public string VarientName3 { get; set; }
        public string VarientValue3 { get; set; }
        public string VarientName4 { get; set; }
        public string VarientValue4 { get; set; }
        public string VarientName5 { get; set; }
        public string VarientValue5 { get; set; }
        public string VarientName6 { get; set; }
        public string VarientValue6 { get; set; }
        public string VarientName7 { get; set; }
        public string VarientValue7 { get; set; }
        public string VarientName8 { get; set; }
        public string VarientValue8 { get; set; }
        public string VarientName9 { get; set; }
        public string VarientValue9 { get; set; }
        public string VarientName10 { get; set; }
        public string VarientValue10 { get; set; }
    }

    public class ListProductDetails
    {
        public List<clsProductVarient> Rows { get; set; }
    }
}
