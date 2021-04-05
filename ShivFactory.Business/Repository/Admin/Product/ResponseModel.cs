using ShivFactory.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository.Admin
{
    #region ProductModel
    public class ProductModel: PaginationRequest
    {
        public bool? ApprovedStatus { get; set; }

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

    #region ProductBasicInfoResponse
    public class ProductBasicInfoResponse
    {
        public string VendorName { get; set; }
        public decimal SalePrice { get; set; }
        public decimal ListPrice { get; set; }
        public decimal LocalShipingCharge { get; set; }
        public decimal ZonalShipingCharge { get; set; }
        public decimal NationalShippingCharge { get; set; }
        public string EstimateDeliveryTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsReturnable { get; set; }
        public int ReturnDays { get; set; }
    }
    #endregion

    #region ProductDetailResponse
    public class ProductDetailResponse
    {
        public string MgfDate { get; set; }
        public string MgfDetail { get; set; }
        public string ShellLife { get; set; }
        public int StockCount { get; set; }
        public string ProductWarning { get; set; }
        public string Description { get; set; }
        public string ProductColors { get; set; }
    }
    #endregion

    #region ProductDimensionResponse
    public class ProductDimensionResponse
    {
        public string ProductLength { get; set; }
        public string ProductWidth { get; set; }
        public string ProductHeight { get; set; }
        public string ProductWeight { get; set; }
        public string PackageLength { get; set; }
        public string PackageWidth { get; set; }
        public string PackageHeight { get; set; }
        public string PackageWeight { get; set; }

    }
    #endregion
}
