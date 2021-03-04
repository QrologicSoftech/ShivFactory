using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
    #region ProductResponse
    public class ProductResponse
    {
        public int SrNo { get; set; }
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string ProductName { get; set; }
        public decimal SalePrice { get; set; }
        public decimal ListPrice { get; set; }
        public decimal LocalShipingCharge { get; set; }
        public decimal ZonalShipingCharge { get; set; }
        public decimal NationalShippingCharge { get; set; }
        public int StockCount { get; set; }
        public string EstimateDeliveryTime { get; set; }
        public int ReturnDays { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string BrandName { get; set; }
        public bool IsActive { get; set; }
        public string AddDate { get; set; }
        public bool ApprovedByAdmin { get; set; }
        public string InactiveReason { get; set; }
    }
    #endregion

    #region UnApprovedProductResponse
    public class UnApprovedProductResponse
    {
        public int SrNo { get; set; }
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string BrandName { get; set; }
        public string AddDate { get; set; }
        public string InactiveReason { get; set; }
    }
    #endregion
}
