using ShivFactory.Business.Repository;
using ShivFactory.Business.Repository.Common;
using ShivFactory.Business.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;

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
        public void VenderId()
        {

        }

        #region Product

        public ActionResult Product()
        {
            return View();
        }
        public ActionResult ProductPartialView()
        {
            try
            {
                
               int venderId= repoVender.GetVendorIdByUserId(utils.GetCurrentUserId());
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
                RepoCategory repoCategory = new RepoCategory();
                ViewBag.category = repoCategory.GetCategoryDDl();
                RepoSubcategory reposubCategory = new RepoSubcategory();
                ViewBag.subcategory = reposubCategory.GetSubCategoryDDl();
                RepoMinicategory repominiCategory = new RepoMinicategory();
                ViewBag.minicategory = repominiCategory.GetMiniCategoryDDl();
                if (id > 0)
                {
                    RepoProduct repoProduct = new RepoProduct();
                    var Product = repoProduct.GetProductByProductId(id.Value);
                    return View(Product);
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            ClsProduct model = new ClsProduct();
            model.VendorId= repoVender.GetVendorIdByUserId(utils.GetCurrentUserId());
            return View(model);
        }
        [HttpPost]
        public ActionResult AddProduct(ClsProduct model, HttpPostedFileBase postedfile)
        {
            try
            {
                RepoCategory repoCategory = new RepoCategory();
                ViewBag.category = repoCategory.GetCategoryDDl();
                RepoSubcategory reposubCategory = new RepoSubcategory();
                ViewBag.subcategory = reposubCategory.GetSubCategoryDDl();
                RepoMinicategory repominiCategory = new RepoMinicategory();
                ViewBag.minicategory = repominiCategory.GetMiniCategoryDDl();

                RepoDimension repodim = new RepoDimension();
                RepoWeightMaster repoweigth = new RepoWeightMaster();
                 ViewBag.dimension = repodim.GetDimensionDDl();
                 ViewBag.weight = repoweigth.GetweightDDl(); 

                if (ModelState.IsValid)
                {
                    return View(model);
                }

            if (model.ProductId == 0 && postedfile == null)
               {
                   ModelState.AddModelError("PostedFile", "Please upload Product Image.");
                  return View(model);
               }
                RepoCommon common = new RepoCommon();
                if (postedfile != null)
                {
                    model.MainImage = common.SaveImage(postedfile); 
                }
               
                foreach( var file in model.files)
                {
                    //model.Image1 = common.SaveImage(postedfile);
                }

                List<string> imagePathlist = new List<string>();
                if  (model.files!=null)
                {
                    model.imgPathList = common.SaveProductMultipleImage(model.files);
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
                if (isDelete)
                {
                    TempData["SuccessMessage"] = "Product deleted successfully!!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failled to delete Product";
                }
                return RedirectToAction("Product", "Vendor");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Product", "Vendor");
            }
        }

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
    }
}