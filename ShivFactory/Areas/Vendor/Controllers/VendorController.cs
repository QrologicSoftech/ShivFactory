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
                    var Product = repoProduct.GetProductByProductId(Convert.ToInt32(id));
                    return View(Product);
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            ClsProduct model = new ClsProduct();
            model.VendorId = repoVender.GetVendorIdByUserId(utils.GetCurrentUserId());
            return View(model);
        }
        [HttpPost]
        public ActionResult AddProduct(ClsProduct model, HttpPostedFileBase postedfile)
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

                //RepoDimension repodim = new RepoDimension();
                //RepoWeightMaster repoweigth = new RepoWeightMaster();
                //ViewBag.dimension = repodim.GetDimensionDDl();
                //ViewBag.weight = repoweigth.GetweightDDl();

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
                    if (image == null && model.ProductId == 0)
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
                var isSaved = repoProduct.AddOrUpdateProduct(model);

                if (isSaved)
                {
                    TempData["SuccessMessage"] = "Product add or update successfully!!";
                    return RedirectToAction("Product", "Vendor");
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
            catch(Exception ex)
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

        #endregion

        #region Product New

        public ActionResult AddProduct1(int? id)
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
        public ActionResult AddProduct1(ClsProduct1 model, HttpPostedFileBase postedfile)
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

                //RepoDimension repodim = new RepoDimension();
                //RepoWeightMaster repoweigth = new RepoWeightMaster();
                //ViewBag.dimension = repodim.GetDimensionDDl();
                //ViewBag.weight = repoweigth.GetweightDDl();

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
                    if (image == null && model.ProductId == 0)
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
                var isSaved = repoProduct.AddOrUpdateProduct(model);

                if (isSaved)
                {
                    TempData["SuccessMessage"] = "Product add or update successfully!!";
                    return RedirectToAction("Product", "Vendor");
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
        public ActionResult ProductVarient()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProductVarient(ProductVarient model)
        {
            return View(model);
        }
        #endregion
    }
}