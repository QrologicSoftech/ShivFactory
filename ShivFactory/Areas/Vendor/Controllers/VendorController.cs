using ShivFactory.Business.Repository;
using ShivFactory.Business.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;
using ShivFactory.Business.Model.Common;
using DataLibrary.DL;
using System.Web.Script.Serialization;

namespace ShivFactory.Areas.Vendor.Controllers
{
    [Authorize(Roles = "Vendor")]
    public class VendorController : Controller
    {
        #region Services
        Utility utils = new Utility();
        RepoVendor repoVender = new RepoVendor();
        #endregion

        // GET: Vendor/Vendor
        public ActionResult Index()
        {
            return View();
        }
        public int GetVendorId()
        {
            return repoVender.GetVendorIdByUserId(utils.GetCurrentUserId());
        }

        #region Product

        public ActionResult Product()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoadProductData()
        {
            try
            {
                // Initialization.  
                var search = Request.Form.GetValues("search[value]")[0];
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                //Find Order Column  
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

                // Prepair model  
                PaginationRequest model = new PaginationRequest()
                {
                    searchText = search,
                    Skip = start != null ? Convert.ToInt32(start) : 0,
                    PageSize = length != null ? Convert.ToInt32(length) : 0,
                    SortColumn = sortColumn,
                    SortDirection = sortColumnDir
                };
                int recordsTotal = 0;
                int venderId = GetVendorId();
                RepoProduct repoProduct = new RepoProduct();
                var productList = repoProduct.GetAllProductsByVendorId(venderId, model, out recordsTotal);

                return Json(new { data = productList, draw = draw, recordsFiltered = productList.Count(), recordsTotal = recordsTotal }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = "", draw = Request.Form.GetValues("draw").FirstOrDefault(), recordsFiltered = 0, recordsTotal = 0, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ProductPartialView()
        {
            try
            {

                int venderId = repoVender.GetVendorIdByUserId(utils.GetCurrentUserId());
                RepoProduct repoProduct = new RepoProduct();
                var categories = repoProduct.GetAllProduct(venderId);
                return View(categories);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(new List<DataLibrary.DL.Product>());
            }
        }
     
        public ActionResult DeleteProduct(int id)
        {

            try
            {
                RepoProduct repoProduct = new RepoProduct();
                var isDelete = repoProduct.DeleteProductByProductId(id);

                return Json(new ResultModel
                {
                    ResultFlag = isDelete,
                    Data = null,
                    Message = isDelete ? "Product deleted successfully!!" : "Failled to delete product."
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultModel
                {
                    ResultFlag = false,
                    Data = null,
                    Message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ProductColors(int productId)
        {
            try
            {
                ViewBag.ProductId = productId;
                RepoProduct repoProduct = new RepoProduct();
                ViewBag.ProductColors = repoProduct.GetProductColorByProductId(productId);
                RepoColor repoColor = new RepoColor();
                var colors = repoColor.GetAllColor();
                return View(colors);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(new List<ColorResponse>());
            }
        }
        #region  Update Product Colors
        public ActionResult UpdateProductColor(int productId, string colors)
        {
            try
            {
                RepoProduct repotProduct = new RepoProduct();
                var isApproved = repotProduct.UpdateProductColorByProductId(productId, colors);
                return Json(new ResultModel
                {
                    ResultFlag = isApproved,
                    Data = null,
                    Message = isApproved ? "Product colors successfully updated!!" : "Failled to update product colors."
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultModel
                {
                    ResultFlag = false,
                    Data = null,
                    Message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        public ActionResult GetSubcategoryByCategoryId(string categoryId)
        {

            int id = 0;
            Int32.TryParse(categoryId, out id);
            RepoSubcategory _repository = new RepoSubcategory();
            var subCategory = _repository.GetSubCategoryDDl(id);
            return Json(subCategory, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMinicategoryBySubCategoryId(string subcategoryId)
        {

            int id = 0;
            Int32.TryParse(subcategoryId, out id);
            RepoMinicategory _repository = new RepoMinicategory();
            var subCategory = _repository.GetMiniCategoryDDl(id);
            return Json(subCategory, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region CheckProductCode
        public ActionResult CheckProductCode(string productCode)
        {
            try
            {
                RepoProduct repoProduct = new RepoProduct();
                bool result = repoProduct.IsExistProductCode(productCode, GetVendorId());

                return Json(new ResultModel
                {
                    ResultFlag = result,
                    Data = null,
                    Message = result == true ? "Product Code already exist!!" : "Product Code available"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultModel
                {
                    ResultFlag = false,
                    Data = null,
                    Message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Product New
        public ActionResult AddProduct(int? id)
        {
            try
            {
                RepoBrand repoBrand = new RepoBrand();
                ViewBag.Brands = repoBrand.GetBrandDDl();
                RepoCategory repoCategory = new RepoCategory();
                ViewBag.Category = repoCategory.GetCategoryDDl();
                RepoSubcategory reposubCategory = new RepoSubcategory();
                ViewBag.SubCategory = reposubCategory.GetSubCategoryDDl();
                RepoMinicategory repominiCategory = new RepoMinicategory();
                ViewBag.MiniCategory = repominiCategory.GetMiniCategoryDDl();
                if (id > 0)
                {
                    RepoProduct repoProduct = new RepoProduct();
                    var Product = repoProduct.GetProductByProductIdNew(Convert.ToInt32(id));
                    return View(Product);
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            ClsProduct1 model = new ClsProduct1();
            model.VendorId = repoVender.GetVendorIdByUserId(utils.GetCurrentUserId());
            return View(model);
        }
        [HttpPost]
        public ActionResult AddProduct(ClsProduct1 model, HttpPostedFileBase postedfile)
        {
            try
            {
                RepoBrand repoBrand = new RepoBrand();
                ViewBag.Brands = repoBrand.GetBrandDDl();
                RepoCategory repoCategory = new RepoCategory();
                ViewBag.Category = repoCategory.GetCategoryDDl();
                RepoSubcategory reposubCategory = new RepoSubcategory();
                ViewBag.SubCategory = reposubCategory.GetSubCategoryDDl();
                RepoMinicategory repominiCategory = new RepoMinicategory();
                ViewBag.MiniCategory = repominiCategory.GetMiniCategoryDDl();

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (model.ProductId == 0 && postedfile == null)
                {
                    ModelState.AddModelError("PostedFile", "Please upload Product Image.");
                    return View(model);
                }
                int index = 0;
                bool isReturn = false;
                foreach (var image in model.files)
                {
                    if (image == null && model.ProductId == null)
                    {
                        isReturn = true;
                        ModelState.AddModelError($"Image{index + 1}", "Please upload Product Image.");
                    }
                    index++;
                }
                if (isReturn) { return View(model); }

                RepoCommon common = new RepoCommon();
                if (postedfile != null)
                {
                    model.MainImage = common.SaveImage(postedfile);
                }

                index = 0;
                foreach (var file in model.files)
                {
                    if (file != null)
                    {
                        string imagePath = common.SaveImage(file);
                        if (index == 0) { model.Image1 = imagePath; }
                        else if (index == 1) { model.Image2 = imagePath; }
                        else if (index == 2) { model.Image3 = imagePath; }
                        else if (index == 3) { model.Image4 = imagePath; }
                        else if (index == 4) { model.Image5 = imagePath; }
                        else if (index == 5) { model.Image6 = imagePath; }
                    }
                    index++;
                }

                RepoProduct repoProduct = new RepoProduct();
                var product = new Product()
                {
                    ProductId = model.ProductId == null ? 0 : model.ProductId.Value,
                    productCode = model.ProductCode,
                    VendorId = model.VendorId,
                    ProductName = model.ProductName,
                    SalePrice = model.SalePrice,
                    ListPrice = model.ListPrice,
                    LocalShipingCharge = model.LocalShipingCharge,
                    ZonalShipingCharge = model.ZonalShipingCharge,
                    NationalShippingCharge = model.NationalShippingCharge,
                    StockCount = model.StockCount,
                    MgfDate = model.MgfDate != null ? Convert.ToDateTime(model.MgfDate) : new DateTime(),
                    MgfDetail = model.MgfDetail,
                    ShellLife = model.ShellLife,
                    ProductWarning = model.ProductWarning,
                    Description = model.Description,
                    EstimateDeliveryTime = model.EstimateDeliveryTime,
                    MainImage = model.MainImage,
                    Image1 = model.Image1,
                    Image2 = model.Image2,
                    Image3 = model.Image3,
                    Image4 = model.Image4,
                    Image5 = model.Image5,
                    Image6 = model.Image6,
                    CategoryId = model.CategoryId,
                    SubCategoryId = model.SubCategoryId,
                    MiniCategoryId = model.MiniCategoryId,
                    IsActive = model.IsActive,
                    ApprovedByAdmin = null,
                    IsReturnable = model.IsReturnable,
                    ReturnDays = model.ReturnDays,
                    AddDate = DateTime.Now,
                    paymentModeCash = model.paymentModeCash
                  
                };
                var isSaved = repoProduct.AddOrUpdateProduct(product);

                if (isSaved)
                {
                    TempData["SuccessMessage"] = "Product add or update successfully!!";
                    return RedirectToAction("ProductVarient","Vendor",new { id=product.ProductId} );
                }
                else
                {
                    TempData["ErrorMessage"] = "Failled to add or update Product";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }
        }
        #endregion

        #region product Varient
        public ActionResult ProductVarient(int id)
        {

            RepoProduct repoproduct = new RepoProduct();
            ClsProduct1 objProductvarient = repoproduct.GetProductByProductIdNew(id); 

            var varientModel = new clsProductVarient
            {
                ProductId = (int)objProductvarient.ProductId,
                ProductQty = objProductvarient.StockCount,
                SalePrice = objProductvarient.SalePrice,
                ListPrice = objProductvarient.ListPrice,
                SubCategoryId = objProductvarient.SubCategoryId
            };
            RepoVarient varient = new RepoVarient();
            var varientddl = varient.GetVarientDDl((int)varientModel.SubCategoryId, null);
            ViewBag.Varient = varientddl;
            return View(varientModel);
        }


        public ActionResult SaveProductVarients(string Rows)
        {
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                List<clsProductVarient> productVarients = js.Deserialize<List<clsProductVarient>>(Rows);
                RepoProduct repoProduct = new RepoProduct();
                foreach (var row in productVarients)
                {
                    var productVarient = new ProductVarient
                    {
                        ProductId = row.ProductId,
                        VarientName1 = row.VarientName1,
                        VarientValue1 = row.VarientValue1,
                        VarientName2 = row.VarientName2,
                        VarientValue2 = row.VarientValue2,
                        VarientName3 = row.VarientName3,
                        VarientValue3 = row.VarientValue3,
                        VarientName4 = row.VarientName4,
                        VarientValue4 = row.VarientValue4,
                        VarientName5 = row.VarientName5,
                        VarientValue5 = row.VarientValue5,
                        VarientName6 = row.VarientName6,
                        VarientValue6 = row.VarientValue6,
                        VarientName7 = row.VarientName7,
                        VarientValue7 = row.VarientValue7,
                        VarientName8 = row.VarientName8,
                        VarientValue8 = row.VarientValue8,
                        VarientName9 = row.VarientName9,
                        VarientValue9 = row.VarientValue9,
                        VarientName10 = row.VarientName10,
                        VarientValue10 = row.VarientValue10,
                        SalePrice = row.SalePrice,
                        ListPrice = row.ListPrice,
                        Stock = row.ProductQty,
                        AddDate = DateTime.Now
                    };
                    repoProduct.AddOrUpdateProductVarient(productVarient);
                }
                return Json(new ResultModel
                {
                    ResultFlag = true,
                    Data = true,
                    Message = "Varients Added successfully!!"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new ResultModel
                {
                    ResultFlag = false,
                    Data = null,
                    Message = e.Message.ToString(),
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region GetVarientDdlByCategoryId
        [AllowAnonymous]
        public ActionResult GetVarientDdlByCategoryId(int SubcategoryId, string varients)
        {
            try
            {
                RepoVarient varient = new RepoVarient();
                var varientddl = varient.GetVarientDDl(SubcategoryId, varients);

                return Json(new ResultModel
                {
                    ResultFlag = true,
                    Data = varientddl,
                    Message = "Varients found successfully!!"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultModel
                {
                    ResultFlag = false,
                    Data = null,
                    Message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region VendorShipingArea
        public ActionResult VendorShippingArea()
        {
           
            return View(); 
        }
        

        public ActionResult AddShippingArea(int? id)
        {
            Utility utility = new Utility();
            RepoVendor vendor = new RepoVendor();
            VendorShippingAreaModel shippingarea = new VendorShippingAreaModel();
            var vendorDetail = vendor.GetVendorDetailsByUserId(utility.GetCurrentUserId());
            shippingarea.VendorName = Convert.ToString(vendorDetail.FirmName);
            if (id > 0)
            {
                var vendorshippingarea = vendor.GetPincodeByID(Convert.ToInt32(id));
                shippingarea.Pincode = vendorshippingarea.pincode;
                shippingarea.VendorId = vendorshippingarea.vendorId.Value;
                shippingarea.ID = vendorshippingarea.ID;
                return View(shippingarea);
            }
       
            shippingarea.VendorId = vendorDetail.VendorId;
            return View(shippingarea);
        }


        [HttpPost]
        public ActionResult AddShippingArea(VendorShippingAreaModel model)
        {
            RepoVendor vendor = new RepoVendor();
            try
            {
                var vendorshiparea = new VendorShippingArea()
                {
                    vendorId = model.VendorId,
                    pincode = model.Pincode
                };
                var isSaved = vendor.AddorUpdateShippingArea(vendorshiparea);
                if (isSaved)
                {
                    TempData["SuccessMessage"] = "Pincode added successfully!!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failled to add pincode";
                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return View(model);
            }
           return RedirectToAction("VendorShippingArea", "Vendor");
            //return View(model);
        }
        public ActionResult VendorShippingAreaPartialView()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult LoadPincodeData()
        {
            try
            {
                // Initialization.  
                var search = Request.Form.GetValues("search[value]")[0];
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                //Find Order Column  
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

                // Prepair model  
                PaginationRequest model = new PaginationRequest()
                {
                    searchText = search,
                    Skip = start != null ? Convert.ToInt32(start) : 0,
                    PageSize = length != null ? Convert.ToInt32(length) : 0,
                    SortColumn = sortColumn,
                    SortDirection = sortColumnDir
                };
                int recordsTotal = 0;
                int venderId = GetVendorId();
                RepoVendor repovendor = new RepoVendor();
                var pincodeList = repovendor.GetAllPincodeByVendorId(venderId, model, out recordsTotal);
                return Json(new { data = pincodeList, draw = draw, recordsFiltered = pincodeList.Count(), recordsTotal = recordsTotal }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = "", draw = Request.Form.GetValues("draw").FirstOrDefault(), recordsFiltered = 0, recordsTotal = 0, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeletePincode(int id)
        {

            try
            {
                RepoVendor repovendor = new RepoVendor();
                var isDelete = repovendor.DeletePincodeByPincodeId(id);

                return Json(new ResultModel
                {
                    ResultFlag = isDelete,
                    Data = null,
                    Message = isDelete ? "Pincode deleted successfully!!" : "Failled to delete Pincode."
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultModel
                {
                    ResultFlag = false,
                    Data = null,
                    Message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}