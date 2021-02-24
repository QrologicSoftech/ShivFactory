using DataLibrary.DL;
using ShivFactory.Business.Model;
using ShivFactory.Business.Models.Other;
using ShivFactory.Business.Repository;
using ShivFactory.Business.Repository.ColorMaster;
using ShivFactory.Business.Repository.Common;
using ShivFactory.Business.Repository.DimensionMaster;
using ShivFactory.Business.Repository.WeightMaster;
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
                    TempData["ErrorMessage"] = "Invalid Details!!";
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
                RepoSubcategory repoCategory = new RepoSubcategory();
                ViewBag.SubCategoryId = repoCategory.GetSubCategoryDDl();

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
                RepoSubcategory repoCategory = new RepoSubcategory();
                ViewBag.SubCategoryId = repoCategory.GetSubCategoryDDl();
                if (!ModelState.IsValid)
                {                    
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
                    return RedirectToAction("MiniCategory", "Admin");
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
                    TempData["SuccessMessage"] = "MiniCategory deleted successfully!!";
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
                return RedirectToAction("MiniCategory", "Admin");
            }
        }

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

        #region DimensionMaster

        public ActionResult DimensionMaster()
        {
            return View();
        }
        public ActionResult DimensionMasterPartialView()
        {
            try
            {
                RepoDimension rsCategory = new RepoDimension();
                var dimensions = rsCategory.GetAllDimensionMaster();
                return View(dimensions);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(new List<DimensionMaster>());
            }
        }
        public ActionResult AddDimensionMaster(int? id)
        {
            try
            {
                RepoDimension rsDimension = new RepoDimension();
                ViewBag.Dimension = rsDimension.GetDimensionMasterDDl();

                if (id > 0)
                {
                    RepoDimension rsCategory = new RepoDimension();
                    var dimension = rsCategory.GetDimensionMasterById(id.Value);
                    return View(dimension);
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return View();
        }
        [HttpPost]
        public ActionResult AddDimensionMaster(DimensionModel model)
        {
            try
            {
                RepoDimension rsDimension = new RepoDimension();
                ViewBag.Dimension = rsDimension.GetDimensionMasterDDl();
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var isSaved = rsDimension.AddOrUpdateDimensionMaster(model);

                if (isSaved)
                {
                    TempData["SuccessMessage"] = "Dimension add or update successfully!!";
                    return RedirectToAction("DimensionMaster", "Admin");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failled to add or update Dimension";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }
        }

        public ActionResult DeleteDimensionMaster(int id)
        {
            try
            {
                RepoDimension rsDimension = new RepoDimension();
                var isDelete = rsDimension.DeleteDimensionMasterById(id);
                if (isDelete)
                {
                    TempData["SuccessMessage"] = "Dimension deleted successfully!!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failled to delete Dimension";
                }
                return RedirectToAction("DimensionMaster", "Admin");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("DimensionMaster", "Admin");
            }
        }


        #endregion


        #region WeightMaster

        public ActionResult WeightMaster()
        {
            return View();
        }
        public ActionResult WeightMasterMasterPartialView()
        {
            try
            {
                RepoWeightMaster rsCategory = new RepoWeightMaster();
                var weight = rsCategory.GetAllWeightMaster();
                return View(weight);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(new List<DimensionMaster>());
            }
        }
        public ActionResult AddWeightMaster(int? id)
        {
            try
            {
                RepoWeightMaster rsCategory = new RepoWeightMaster();
                ViewBag.Dimension = rsCategory.GetweightMasterDDl();

                if (id > 0)
                {
                    var dimension = rsCategory.GetWeightMasterById(id.Value);
                    return View(dimension);
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return View();
        }
        [HttpPost]
        public ActionResult AddWeightMaster(WeightModel model)
        {
            try
            {
                RepoWeightMaster rsDimension = new RepoWeightMaster();
                ViewBag.Dimension = rsDimension.GetweightMasterDDl();
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var isSaved = rsDimension.AddOrUpdateWeightMaster(model);

                if (isSaved)
                {
                    TempData["SuccessMessage"] = "Weight add or update successfully!!";
                    return RedirectToAction("WeightMaster", "Admin");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failled to add or update Dimension";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }
        }

        public ActionResult DeleteWeightMaster(int id)
        {
            try
            {
                RepoWeightMaster rsDimension = new RepoWeightMaster();
                var isDelete = rsDimension.DeleteWeightMasterById(id);
                if (isDelete)
                {
                    TempData["SuccessMessage"] = "Weight deleted successfully!!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failled to delete Weight";
                }
                return RedirectToAction("WeightMaster", "Admin");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("WeightMaster", "Admin");
            }
        }


        #endregion

        #region ColorMaster

        public ActionResult ColorMaster()
        {
            return View();
        }
        public ActionResult ColorMasterPartialView()
        {
            try
            {
                RepoColorMaster rsCategory = new RepoColorMaster();
                var weight = rsCategory.GetAllColorMaster();
                return View(weight);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(new List<DimensionMaster>());
            }
        }
        public ActionResult AddColorMaster(int? id)
        {
            try
            {
                RepoColorMaster rsCategory = new RepoColorMaster();
                ViewBag.Dimension = rsCategory.GetColorMasterDDl();

                if (id > 0)
                {
                    var dimension = rsCategory.GetColorMasterById(id.Value);
                    return View(dimension);
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return View();
        }
        [HttpPost]
        public ActionResult AddColorMaster(ColorModel model)
        {
            try
            {
                RepoColorMaster rsDimension = new RepoColorMaster();
                ViewBag.Dimension = rsDimension.GetAllColorMaster();
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var isSaved = rsDimension.AddOrUpdateColorMaster(model);

                if (isSaved)
                {
                    TempData["SuccessMessage"] = "Color add or update successfully!!";
                    return RedirectToAction("ColorMaster", "Admin");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failled to add or update Color";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }
        }

        public ActionResult DeleteColorMaster(int id)
        {
            try
            {
                RepoColorMaster rsDimension = new RepoColorMaster();
                var isDelete = rsDimension.DeleteColorMasterById(id);
                if (isDelete)
                {
                    TempData["SuccessMessage"] = "Color deleted successfully!!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failled to delete Color";
                }
                return RedirectToAction("ColorMaster", "Admin");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ColorMaster", "Admin");
            }
        }


        #endregion
    }
}