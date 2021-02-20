using DataLibrary.DL;
using ShivFactory.Business.Factory.Services;
using ShivFactory.Business.Model;
using ShivFactory.Business.Models.Other;
using ShivFactory.Business.Repository;
using ShivFactory.Business.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShivFactory.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        UserService user = new UserService();
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

                    ModelState.AddModelError("CatImage", "Please select Category Image.");
                    return View(model);
                }

                // save image file
                RepoCommon common = new RepoCommon();
                model.ImagePath = common.SaveImage(postedfile);

                RepoCategory repoCategory = new RepoCategory();
                var isSaved = repoCategory.AddOrUpdateCategory(model);

                if (isSaved)
                {
                    TempData["SuccessMessage"] = "Category add or update successfully!!";
                    return RedirectToAction("Category", "Admin");
                }
                else
                {
                    TempData["SuccessMessage"] = "Failled to add or update category";
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
            var subcategory = user.GetAllSubCategory();
            return View(subcategory);
        }
        public ActionResult AddSubCategory(int? id)
        {
            RepoCategory repoCategory = new RepoCategory();
            ViewBag.category = repoCategory.GetCategoryDDl();
            if (id > 0)
            {
                var editSubCategory = user.GetSubCategoryById(id.Value);
                return View("AddSubCategory", editSubCategory);
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddSubCategory(SubCategoryModel model, HttpPostedFileBase postedfile)
        {
            //try
            //{
            if (ModelState.IsValid)
            {
                if (model.ID == 0)
                {
                    if (postedfile != null)
                    {
                        //model.SubCatImage = user.SaveImage(postedfile);
                    }
                    //else
                    //{
                    //    model.CatImage=
                    //}
                    var id = user.SaveSubCategory(model);
                    if (id > 0)
                    {
                        ModelState.Clear();
                        return RedirectToAction("SubCategory", "Admin");
                    }
                }
                else
                {
                    if (postedfile != null)
                    {
                        //model.SubCatImage = user.SaveImage(postedfile);
                    }
                    var update = user.UpdateSubCategory(model.ID, model);
                    TempData["message"] = "Added";
                    return RedirectToAction("SubCategory", "Admin");
                }
            }
            return RedirectToAction("SubCategory", "Admin");
            //}
            //catch (Exception ex)
            //{
            //    return View();
            //}
        }

        public ActionResult DeleteSubCategory(int id)
        {
            var deletecat = user.RemoveSubCategory(id);
            return RedirectToAction("SubCategory", "Admin");
        }

        #endregion

        #region SubCategory

        public ActionResult MiniCategory()
        {
            return View();
        }
        public ActionResult MiniCategoryPartialView()
        {
            var subcategory = user.GetAllMiniCategory();
            return View(subcategory);
        }
        public ActionResult AddMiniCategory(int? id)
        {
            ViewBag.MiniCategory = new SelectList(user.GetAllSubCategory(), "ID", "SubCatName");
            if (id > 0)
            {
                var editMiniCategory = user.GetMiniCategoryById(id.Value);
                return View("AddMiniCategory", editMiniCategory);
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddMiniCategory(MiniCategoryModel model, HttpPostedFileBase postedfile)
        {
            //try
            //{
            if (ModelState.IsValid)
            {
                if (model.ID == 0)
                {
                    if (postedfile != null)
                    {
                        model.MiniCatImage = user.SaveImage(postedfile);
                    }
                    //else
                    //{
                    //    model.CatImage=
                    //}
                    var id = user.SaveMiniCategory(model);

                    if (id > 0)
                    {
                        TempData["message"] = "Sub Category Added Successfully!";
                        ModelState.Clear();
                        return RedirectToAction("MiniCategory", "Admin");
                    }
                }
                else
                {
                    if (postedfile != null)
                    {
                        model.MiniCatImage = user.SaveImage(postedfile);
                    }
                    var update = user.UpdateMiniCategory(model.ID, model);
                    TempData["message"] = "Sub Category updated Successfully!";
                    return RedirectToAction("MiniCategory", "Admin");
                }
            }
            return RedirectToAction("MiniCategory", "Admin");
            //}
            //catch (Exception ex)
            //{
            //    return View();
            //}
        }

        public ActionResult DeleteMiniCategory(int id)
        {
            var deletecat = user.RemoveMiniCategory(id);
            TempData["message"] = "Category Deleted Successfully";
            return RedirectToAction("MiniCategory", "Admin");
        }

        #endregion

        #region Brand

        public ActionResult Brand()
        {
            return View();
        }
        public ActionResult BrandPartialView()
        {
            var brand = user.GetAllBrand();
            return View(brand);
        }
        public ActionResult AddBrand(int? id)
        {
            if (id > 0)
            {
                var editBrand = user.GetBrandById(id.Value);
                return View("AddBrand", editBrand);
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddBrand(BrandModel model, HttpPostedFileBase postedfile)
        {
            //try
            //{
            if (ModelState.IsValid)
            {
                if (model.ID == 0)
                {
                    if (postedfile != null)
                    {
                        model.BrandImage = user.SaveImage(postedfile);
                    }
                    //else
                    //{
                    //    model.CatImage=
                    //}
                    var id = user.SaveBrand(model);
                    if (id > 0)
                    {
                        ModelState.Clear();
                        return RedirectToAction("Brand", "Admin");
                    }
                }
                else
                {
                    if (postedfile != null)
                    {
                        model.BrandImage = user.SaveImage(postedfile);
                    }
                    var update = user.UpdateBrand(model.ID, model);
                    TempData["message"] = "Added";
                    return RedirectToAction("Brand", "Admin");
                }
            }
            return RedirectToAction("Brand", "Admin");
            //}
            //catch (Exception ex)
            //{
            //    return View();
            //}
        }

        public ActionResult DeleteBrand(int id)
        {
            var deletecat = user.RemoveBrand(id);
            return RedirectToAction("Brand", "Admin");
        }

        #endregion

        #region Banner

        public ActionResult Banner()
        {
            return View();
        }
        public ActionResult BannerPartialView()
        {
            var banner = user.GetAllBanner();
            return View(banner);
        }
        public ActionResult AddBanner(int? id)
        {
            if (id > 0)
            {
                var editBanner = user.GetBannerById(id.Value);
                return View("AddMiniCategory", editBanner);
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddBanner(BannerModel model, HttpPostedFileBase postedfile)
        {
            //try
            //{
            if (ModelState.IsValid)
            {
                if (model.ID == 0)
                {
                    if (postedfile != null)
                    {
                        model.BannerImage = user.SaveImage(postedfile);
                    }
                    //else
                    //{
                    //    model.CatImage=
                    //}
                    var id = user.SaveBanner(model);
                    if (id > 0)
                    {
                        ModelState.Clear();
                        return RedirectToAction("Banner", "Admin");
                    }
                }
                else
                {
                    if (postedfile != null)
                    {
                        model.BannerImage = user.SaveImage(postedfile);
                        TempData["message"] = "Banner Added Successfuly!";
                    }
                    var update = user.UpdateBanner(model.ID, model);
                    TempData["message"] = "Banner Updated Successfuly!";
                    return RedirectToAction("Banner", "Admin");
                }
            }
            return RedirectToAction("Banner", "Admin");
            //}
            //catch (Exception ex)
            //{
            //    return View();
            //}
        }

        public ActionResult DeleteBanner(int id)
        {
            var deletecat = user.RemoveBanner(id);
            TempData["message"] = "Banner Removed Successfuly!";
            return RedirectToAction("Banner", "Admin");
        }

        #endregion
    }
}