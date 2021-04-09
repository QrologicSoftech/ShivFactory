using ShivFactory.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository.Admin
{
    #region ProductModel
    public class ProductModel : PaginationRequest
    {
        public int? ApprovedStatus { get; set; }

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
    }
    #endregion

    #region ProductVarientResponse
    public class ProductVarientResponse
    {
        public int Id { get; set; }
        public int ProductQty { get; set; }
        public decimal SalePrice { get; set; }
        public decimal ListPrice { get; set; }
        public string Feature1 { get; set; }
        public string Feature2 { get; set; }
        public string Feature3 { get; set; }
        public string Feature4 { get; set; }
        public string Feature5 { get; set; }
        public string Feature6 { get; set; }
        public string Feature7 { get; set; }
        public string Feature8 { get; set; }
        public string Feature9 { get; set; }
        public string Feature10 { get; set; }
        public string Image { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }

    }
    #endregion
}
