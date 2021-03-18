using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ShivFactory.Business.Repository
{
    public class ClsProduct
    {
        public int ProductId { get; set; }
        public int? VendorId { get; set; }
        [Required(ErrorMessage = "Product Name required"), MaxLength(50, ErrorMessage = "ProductName cannot be longer than 50 characters.")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "SalePrice required"), DataType(DataType.Currency, ErrorMessage = "SalePrice is a decimal value.")]
        public decimal SalePrice { get; set; }
        [Required(ErrorMessage = "ListPrice required"), DataType(DataType.Currency, ErrorMessage = "ListPrice is a decimal value.")]
        public decimal ListPrice { get; set; }
        [Required(ErrorMessage = "Local Shiping Charge required"), DataType(DataType.Currency, ErrorMessage = "Local Shiping Charge is a decimal value.")]
        public decimal LocalShipingCharge { get; set; }
        [Required(ErrorMessage = "Zonal Shiping Charge required"), DataType(DataType.Currency, ErrorMessage = "Zonal Shiping Charge is a decimal value.")]
        public decimal ZonalShipingCharge { get; set; }
        [Required(ErrorMessage = "National Shiping Charge required"), DataType(DataType.Currency, ErrorMessage = "National Shiping Charge is a decimal value.")]
        public decimal NationalShippingCharge { get; set; }
        [Required(ErrorMessage = "Stock required"), Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int StockCount { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string MgfDate { get; set; }
        [MaxLength(100, ErrorMessage = "Field cannot be longer than 100 characters.")]
        public string MgfDetail { get; set; }
        [MaxLength(100, ErrorMessage = "Field cannot be longer than 100 characters.")]
        public string ShellLife { get; set; }
        [MaxLength(200, ErrorMessage = "Field cannot be longer than 200 characters.")]
        public string ProductWarning { get; set; }
        [MaxLength(300, ErrorMessage = "Field cannot be longer than 300 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Delivery time is a required field!"), MaxLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string EstimateDeliveryTime { get; set; }
        public string MainImage { get; set; }

        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        public string Image6 { get; set; }
        [Required(ErrorMessage = "Please select Brand!")]
        public int? BrandId { get; set; }
        [Required(ErrorMessage = "Please select Category!")]
        public int? CategoryId { get; set; }
        [Required(ErrorMessage = "Please select SubCategory!")]
        public int? SubCategoryId { get; set; }
        [Required(ErrorMessage = "Please select MiniCategory!")]
        public int? MiniCategoryId { get; set; }
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "ProductLength is a required field!"), MaxLength(40, ErrorMessage = "Field cannot be longer than 40 characters.")]
        public string ProductLength { get; set; }
        [Required(ErrorMessage = "ProductWidth is a required field!"), MaxLength(40, ErrorMessage = "Field cannot be longer than 40 characters.")]
        public string ProductWidth { get; set; }
        [Required(ErrorMessage = "ProductHeight is a required field!"), MaxLength(40, ErrorMessage = "Field cannot be longer than 40 characters.")]
        public string ProductHeight { get; set; }
        [Required(ErrorMessage = "ProductWeight is a required field!"), MaxLength(40, ErrorMessage = "Field cannot be longer than 40 characters.")]
        public string ProductWeight { get; set; }
        [Required(ErrorMessage = "PackageLength is a required field!"), MaxLength(40, ErrorMessage = "Field cannot be longer than 40 characters.")]
        public string PackageLength { get; set; }
        [Required(ErrorMessage = "PackageWidth is a required field!"), MaxLength(40, ErrorMessage = "Field cannot be longer than 40 characters.")]
        public string PackageWidth { get; set; }
        [Required(ErrorMessage = "PackageHeight is a required field!"), MaxLength(40, ErrorMessage = "Field cannot be longer than 40 characters.")]
        public string PackageHeight { get; set; }
        [Required(ErrorMessage = "PackageWeight is a required field!"), MaxLength(40, ErrorMessage = "Field cannot be longer than 40 characters.")]
        public string PackageWeight { get; set; }
        public string ProductColors { get; set; }
        public bool IsReturnable { get; set; }
        public int ReturnDays { get; set; }
        [Required(ErrorMessage = "ProductCode is a required field and must be Unique!")]
        public string ProductCode { get; set; }
        public List<string> imgPathList { get; set; }
        public HttpPostedFileBase PostedFile { get; set; }
        public List<HttpPostedFileBase> files { get; set; }
    }
    public class ClsProduct1
    {
        public int? ProductId { get; set; }
        public int? VendorId { get; set; }
        [Required(ErrorMessage = "Product Name required"), MaxLength(50, ErrorMessage = "ProductName cannot be longer than 50 characters.")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "SalePrice required"), DataType(DataType.Currency, ErrorMessage = "SalePrice is a decimal value.")]
        public decimal SalePrice { get; set; }
        [Required(ErrorMessage = "ListPrice required"), DataType(DataType.Currency, ErrorMessage = "ListPrice is a decimal value.")]
        public decimal ListPrice { get; set; }
        [Required(ErrorMessage = "Local Shiping Charge required"), DataType(DataType.Currency, ErrorMessage = "Local Shiping Charge is a decimal value.")]
        public decimal LocalShipingCharge { get; set; }
        [Required(ErrorMessage = "Zonal Shiping Charge required"), DataType(DataType.Currency, ErrorMessage = "Zonal Shiping Charge is a decimal value.")]
        public decimal ZonalShipingCharge { get; set; }
        [Required(ErrorMessage = "National Shiping Charge required"), DataType(DataType.Currency, ErrorMessage = "National Shiping Charge is a decimal value.")]
        public decimal NationalShippingCharge { get; set; }
        [Required(ErrorMessage = "Stock required"), Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int StockCount { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string MgfDate { get; set; }
        [MaxLength(100, ErrorMessage = "Field cannot be longer than 100 characters.")]
        public string MgfDetail { get; set; }
        [MaxLength(100, ErrorMessage = "Field cannot be longer than 100 characters.")]
        public string ShellLife { get; set; }
        [MaxLength(200, ErrorMessage = "Field cannot be longer than 200 characters.")]
        public string ProductWarning { get; set; }
        [MaxLength(300, ErrorMessage = "Field cannot be longer than 300 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Delivery time is a required field!"), MaxLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string EstimateDeliveryTime { get; set; }
        public string MainImage { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        public string Image6 { get; set; }
        
        [Required(ErrorMessage = "Please select Category!")]
        public int? CategoryId { get; set; }
        [Required(ErrorMessage = "Please select SubCategory!")]
        public int? SubCategoryId { get; set; }
        [Required(ErrorMessage = "Please select MiniCategory!")]
        public int? MiniCategoryId { get; set; }
        public bool IsActive { get; set; }
        public string ProductColors { get; set; }
        public bool IsReturnable { get; set; }
        public int ReturnDays { get; set; }
        [Required(ErrorMessage = "ProductCode is a required field and must be Unique!")]
        public string ProductCode { get; set; }
        public List<string> imgPathList { get; set; }
        public HttpPostedFileBase PostedFile { get; set; }
        public List<HttpPostedFileBase> files { get; set; }
    }
}
