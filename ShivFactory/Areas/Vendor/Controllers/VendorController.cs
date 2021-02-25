using ShivFactory.Business.Repository;
using ShivFactory.Business.Repository.Common;
using ShivFactory.Business.Repository.Product;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace ShivFactory.Areas.Vendor.Controllers
{
    [Authorize(Roles = "Vendor")]
    public class VendorController : Controller
    {
        // GET: Vendor/Vendor
        public ActionResult Index()
        {
            return View();
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
                RepoProduct repoProduct = new RepoProduct();
                var categories = repoProduct.GetAllProduct();
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

            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(ProductModel model, HttpPostedFileBase postedfile)
        {
            try
            {
                RepoCategory repoCategory = new RepoCategory();
                ViewBag.category = repoCategory.GetCategoryDDl();
                RepoSubcategory reposubCategory = new RepoSubcategory();
                ViewBag.subcategory = reposubCategory.GetSubCategoryDDl();
                RepoMinicategory repominiCategory = new RepoMinicategory();
                ViewBag.minicategory = repominiCategory.GetMiniCategoryDDl();
                if (ModelState.IsValid)
                {
                    return View(model);
                }

                    if (model.ProductId == 0 && postedfile == null)
                {

                    ModelState.AddModelError("PostedFile", "Please upload Product Image.");
                    return View(model);
                }
                if (postedfile != null)
                {
                   RepoCommon common = new RepoCommon();
                    model.ImagePath = common.SaveImage(postedfile); 
                }
                string filestr = "";
                List<string> imagePathlist = new List<string>();
                if  (model.files!=null)
                {
                    RepoCommon common = new RepoCommon();
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
            bool isValid = Int32.TryParse(categoryId, out id);
            RepoSubcategory _repository = new RepoSubcategory();
            var subCategory = _repository.GetSubCategoryDDl(id);
            return Json(subCategory, JsonRequestBehavior.AllowGet);
        }


        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetMinicategoryBySubCategoryId(string subcategoryId)
        {
            
            int id = 0;
            bool isValid = Int32.TryParse(subcategoryId, out id);
            RepoMinicategory _repository = new RepoMinicategory();
            var subCategory = _repository.GetMiniCategoryDDl(id);
            return Json(subCategory, JsonRequestBehavior.AllowGet);
        }

        #endregion


    }
}