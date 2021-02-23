using DataLibrary.DL;
using ShivFactory.Business.Model;
using ShivFactory.Business.Models.Other;
using ShivFactory.Business.Repository;
using ShivFactory.Business.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ShivFactory.FilterConfig;

namespace ShivFactory.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin"), UserSessionActionFilter]
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }

        #region Category

        public ActionResult Category()
        {
            return View();
        }
        public ActionResult CategoryPartialView()
        {
            try
            {
                RepoCategory repoCategory = new RepoCategory();
                var categories = repoCategory.GetAllCategory();
                return View(categories);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(new List<Category>());
            }
        }
        public ActionResult AddCategory(int? id)
        {
            try
            {
                if (id > 0)
                {
                    RepoCategory repoCategory = new RepoCategory();
                    var category = repoCategory.GetCategoryByCategoryId(id.Value);
                    return View(category);
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(CategoryModel model, HttpPostedFileBase postedfile)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (model.CategoryId == 0 && postedfile == null)
                {

                    ModelState.AddModelError("PostedFile", "Please upload Category Image.");
                    return View(model);
                }
                if (postedfile != null)
                {

                    // save image file
                    RepoCommon common = new RepoCommon();
                    model.ImagePath = common.SaveImage(postedfile);
                }

                RepoCategory repoCategory = new RepoCategory();
                var isSaved = repoCategory.AddOrUpdateCategory(model);

                if (isSaved)
                {
                    TempData["SuccessMessage"] = "Category add or update successfully!!";
                    return RedirectToAction("Category", "Admin");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failled to add or update category";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }
        }

        public ActionResult DeleteCategory(int id)
        {
            try
            {
                RepoCategory repoCategory = new RepoCategory();
                var isDelete = repoCategory.DeleteCategoryByCategoryId(id);
                if (isDelete)
                {
                    TempData["SuccessMessage"] = "Category deleted successfully!!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failled to delete category";
                }
                return RedirectToAction("Category", "Admin");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Category", "Admin");
            }
        }

        #endregion

        #region SubCategory

        public ActionResult SubCategory()
        {
            return View();
        }
        public ActionResult SubCategoryPartialView()
        {
            try
            {
                RepoSubcategory rsCategory = new RepoSubcategory();
                var categories = rsCategory.GetAllSubCategory();
                return View(categories);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(new List<SubCategory>());
            }
        }
        public ActionResult AddSubCategory(int? id)
        {
            try
            {
                RepoCategory repoCategory = new RepoCategory();
                ViewBag.category = repoCategory.GetCategoryDDl();

                if (id > 0)
                {
                    RepoSubcategory rsCategory = new RepoSubcategory();
                    var category = rsCategory.GetSubCategoryById(id.Value);
                    return View(category);
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return View();
        }
        [HttpPost]
        public ActionResult AddSubCategory(SubCategoryModel model, HttpPostedFileBase postedfile)
        {
            try
            {
                RepoCategory repoCategory = new RepoCategory();
                ViewBag.category = repoCategory.GetCategoryDDl();
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (model.SubCategoryId == 0 && postedfile == null)
                {
                    ModelState.AddModelError("PostedFile", "Please upload Category Image.");
                    return View(model);
                }
                if (postedfile != null)
                {

                    // save image file
                    RepoCommon common = new RepoCommon();
                    model.ImagePath = common.SaveImage(postedfile);
                }

                RepoSubcategory rsCategory = new RepoSubcategory();
                var isSaved = rsCategory.AddOrUpdateSubCategory(model);

                if (isSaved)
                {
                    TempData["SuccessMessage"] = "SubCategory add or update successfully!!";
                    return RedirectToAction("SubCategory", "Admin");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failled to add or update subcategory";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }
        }

        public ActionResult DeleteSubCategory(int id)
        {
            try
            {
                RepoSubcategory rsCategory = new RepoSubcategory();
                var isDelete = rsCategory.DeleteSubCategoryById(id);
                if (isDelete)
                {
                    TempData["SuccessMessage"] = "SubCategory deleted successfully!!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failled to delete subcategory";
                }
                return RedirectToAction("SubCategory", "Admin");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("SubCategory", "Admin");
            }
        }

        #endregion

        #region MiniCategory

        public ActionResult MiniCategory()
        {
            return View();
        }
        public ActionResult MiniCategoryPartialView()
        {
            try
            {
                RepoMinicategory rsCategory = new RepoMinicategory();
                var categories = rsCategory.GetAllMiniCategory();
                return View(categories);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(new List<MiniCategory>());
            }
        }
        public ActionResult AddMiniCategory(int? id)
        {
            try
            {
                RepoMinicategory repoCategory = new RepoMinicategory();
                ViewBag.subcategory = repoCategory.GetMiniCategoryDDl();

                if (id > 0)
                {
                    RepoMinicategory rsCategory = new RepoMinicategory();
                    var category = rsCategory.GetMiniCategoryById(id.Value);
                    return View(category);
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return View();
        }
        [HttpPost]
        public ActionResult AddMiniCategory(MiniCategoryModel model, HttpPostedFileBase postedfile)
        {
            try
            {
                RepoMinicategory repoCategory = new RepoMinicategory();
                ViewBag.SubCategoryId = repoCategory.GetMiniCategoryDDl();
                if (ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Please Enter Valid Details.";
                    return View(model);
                }

                if (model.MiniCategoryId == 0 && postedfile == null)
                {
                    ModelState.AddModelError("PostedFile", "Please upload Category Image.");
                    return View(model);
                }
                if (postedfile != null)
                {

                    // save image file
                    RepoCommon common = new RepoCommon();
                    model.ImagePath = common.SaveImage(postedfile);
                }

                RepoMinicategory rsCategory = new RepoMinicategory();
                var isSaved = rsCategory.AddOrUpdateMiniCategory(model);

                if (isSaved)
                {
                    TempData["SuccessMessage"] = "MiniCategory add or update successfully!!";
                    return RedirectToAction("SubCategory", "Admin");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failled to add or update Minicategory";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }
        }

        public ActionResult DeleteMiniCategory(int id)
        {
            try
            {
                RepoSubcategory rsCategory = new RepoSubcategory();
                var isDelete = rsCategory.DeleteSubCategoryById(id);
                if (isDelete)
                {
                    TempData["SuccessMessage"] = "SubCategory deleted successfully!!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failled to delete subcategory";
                }
                return RedirectToAction("SubCategory", "Admin");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("SubCategory", "Admin");
            }
        }

        #endregion

        #region MiniCategory

        //public ActionResult MiniCategory()
        //{
        //    return View();
        //}
        //public ActionResult MiniCategoryPartialView()
        //{
        //    var subcategory = user.GetAllMiniCategory();
        //    return View(subcategory);
        //}
        //public ActionResult AddMiniCategory(int? id)
        //{
        //    ViewBag.MiniCategory = new SelectList(user.GetAllSubCategory(), "ID", "SubCatName");
        //    if (id > 0)
        //    {
        //        var editMiniCategory = user.GetMiniCategoryById(id.Value);
        //        return View("AddMiniCategory", editMiniCategory);
        //    }
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult AddMiniCategory(MiniCategoryModel model, HttpPostedFileBase postedfile)
        //{
        //    //try
        //    //{
        //    if (ModelState.IsValid)
        //    {
        //        if (model.ID == 0)
        //        {
        //            if (postedfile != null)
        //            {
        //                model.MiniCatImage = user.SaveImage(postedfile);
        //            }
        //            //else
        //            //{
        //            //    model.CatImage=
        //            //}
        //            var id = user.SaveMiniCategory(model);

        //            if (id > 0)
        //            {
        //                TempData["message"] = "Sub Category Added Successfully!";
        //                ModelState.Clear();
        //                return RedirectToAction("MiniCategory", "Admin");
        //            }
        //        }
        //        else
        //        {
        //            if (postedfile != null)
        //            {
        //                model.MiniCatImage = user.SaveImage(postedfile);
        //            }
        //            var update = user.UpdateMiniCategory(model.ID, model);
        //            TempData["message"] = "Sub Category updated Successfully!";
        //            return RedirectToAction("MiniCategory", "Admin");
        //        }
        //    }
        //    return RedirectToAction("MiniCategory", "Admin");
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    return View();
        //    //}
        //}

        //public ActionResult DeleteMiniCategory(int id)
        //{
        //    var deletecat = user.RemoveMiniCategory(id);
        //    TempData["message"] = "Category Deleted Successfully";
        //    return RedirectToAction("MiniCategory", "Admin");
        //}

        #endregion

        #region Brand

        //public ActionResult Brand()
        //{
        //    return View();
        //}
        //public ActionResult BrandPartialView()
        //{
        //    var brand = user.GetAllBrand();
        //    return View(brand);
        //}
        //public ActionResult AddBrand(int? id)
        //{
        //    if (id > 0)
        //    {
        //        var editBrand = user.GetBrandById(id.Value);
        //        return View("AddBrand", editBrand);
        //    }
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult AddBrand(BrandModel model, HttpPostedFileBase postedfile)
        //{
        //    //try
        //    //{
        //    if (ModelState.IsValid)
        //    {
        //        if (model.ID == 0)
        //        {
        //            if (postedfile != null)
        //            {
        //                model.BrandImage = user.SaveImage(postedfile);
        //            }
        //            //else
        //            //{
        //            //    model.CatImage=
        //            //}
        //            var id = user.SaveBrand(model);
        //            if (id > 0)
        //            {
        //                ModelState.Clear();
        //                return RedirectToAction("Brand", "Admin");
        //            }
        //        }
        //        else
        //        {
        //            if (postedfile != null)
        //            {
        //                model.BrandImage = user.SaveImage(postedfile);
        //            }
        //            var update = user.UpdateBrand(model.ID, model);
        //            TempData["message"] = "Added";
        //            return RedirectToAction("Brand", "Admin");
        //        }
        //    }
        //    return RedirectToAction("Brand", "Admin");
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    return View();
        //    //}
        //}

        //public ActionResult DeleteBrand(int id)
        //{
        //    var deletecat = user.RemoveBrand(id);
        //    return RedirectToAction("Brand", "Admin");
        //}

        #endregion

        #region Banner

        //public ActionResult Banner()
        //{
        //    return View();
        //}
        //public ActionResult BannerPartialView()
        //{
        //    var banner = user.GetAllBanner();
        //    return View(banner);
        //}
        //public ActionResult AddBanner(int? id)
        //{
        //    if (id > 0)
        //    {
        //        var editBanner = user.GetBannerById(id.Value);
        //        return View("AddMiniCategory", editBanner);
        //    }
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult AddBanner(BannerModel model, HttpPostedFileBase postedfile)
        //{
        //    //try
        //    //{
        //    if (ModelState.IsValid)
        //    {
        //        if (model.ID == 0)
        //        {
        //            if (postedfile != null)
        //            {
        //                model.BannerImage = user.SaveImage(postedfile);
        //            }
        //            //else
        //            //{
        //            //    model.CatImage=
        //            //}
        //            var id = user.SaveBanner(model);
        //            if (id > 0)
        //            {
        //                ModelState.Clear();
        //                return RedirectToAction("Banner", "Admin");
        //            }
        //        }
        //        else
        //        {
        //            if (postedfile != null)
        //            {
        //                model.BannerImage = user.SaveImage(postedfile);
        //                TempData["message"] = "Banner Added Successfuly!";
        //            }
        //            var update = user.UpdateBanner(model.ID, model);
        //            TempData["message"] = "Banner Updated Successfuly!";
        //            return RedirectToAction("Banner", "Admin");
        //        }
        //    }
        //    return RedirectToAction("Banner", "Admin");
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    return View();
        //    //}
        //}

        //public ActionResult DeleteBanner(int id)
        //{
        //    var deletecat = user.RemoveBanner(id);
        //    TempData["message"] = "Banner Removed Successfuly!";
        //    return RedirectToAction("Banner", "Admin");
        //}

        #endregion
    }
}