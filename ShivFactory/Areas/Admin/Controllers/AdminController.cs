using DataLibrary.DL;
using ShivFactory.Business.Model;
using ShivFactory.Business.Models.Other;
using ShivFactory.Business.Repository;
using ShivFactory.Business.Repository.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
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
        [HttpPost]
        public ActionResult LoadCategoryData()
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
                RepoCategory repoCategory = new RepoCategory();
                var categoryList = repoCategory.GetAllCategories(model, out recordsTotal);

                return Json(new { data = categoryList, draw = draw, recordsFiltered = categoryList.Count(), recordsTotal = recordsTotal }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = "", draw = Request.Form.GetValues("draw").FirstOrDefault(), recordsFiltered = 0, recordsTotal = 0, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
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
                    var category = repoCategory.GetCategoryByCategoryId(Convert.ToInt32(id));
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
                var isDelete = repoCategory.DeleteCategoryByCategoryId(Convert.ToInt32(id));

                return Json(new ResultModel
                {
                    ResultFlag = isDelete,
                    Data = null,
                    Message = isDelete ? "Category deleted successfully!!" : "Failled to delete category"
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

        #region SubCategory

        public ActionResult SubCategory()
        {
            return View();
        }

        public ActionResult LoadSubCategoryData()
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
                RepoSubcategory reposubCategory = new RepoSubcategory();
                var categoryList = reposubCategory.GetAllSubCategories(model, out recordsTotal);

                return Json(new { data = categoryList, draw = draw, recordsFiltered = categoryList.Count(), recordsTotal = recordsTotal }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = "", draw = Request.Form.GetValues("draw").FirstOrDefault(), recordsFiltered = 0, recordsTotal = 0, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
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
                return View(new List<Brand>());
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
                    var category = rsCategory.GetSubCategoryById(Convert.ToInt32(id));
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
                return Json(new ResultModel
                {
                    ResultFlag = isDelete,
                    Data = null,
                    Message = isDelete ? "SubCategory deleted successfully!!" : "Failled to delete subcategory"
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

        #region MiniCategory

        public ActionResult MiniCategory()
        {
            return View();
        }

        public ActionResult LoadMiniCategoryData()
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
                RepoMinicategory repominiCategory = new RepoMinicategory();
                var categoryList = repominiCategory.GetAllMiniCategories(model, out recordsTotal);

                return Json(new { data = categoryList, draw = draw, recordsFiltered = categoryList.Count(), recordsTotal = recordsTotal }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = "", draw = Request.Form.GetValues("draw").FirstOrDefault(), recordsFiltered = 0, recordsTotal = 0, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
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

                return Json(new ResultModel
                {
                    ResultFlag = isDelete,
                    Data = null,
                    Message = isDelete ? "MiniCategory deleted successfully!!" : "Failled to delete miniCategory"
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

        #region Brand

        public ActionResult Brand()
        {
            return View();
        }
        public ActionResult BrandPartialView()
        {
            return View();
        }
        public ActionResult LoadBrandData()
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
                RepoBrand repoBrand = new RepoBrand();
                var brands = repoBrand.GetAllBrands(model, out recordsTotal);

                return Json(new { data = brands, draw = draw, recordsFiltered = brands.Count(), recordsTotal = recordsTotal }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = "", draw = Request.Form.GetValues("draw").FirstOrDefault(), recordsFiltered = 0, recordsTotal = 0, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AddBrand(int? id)
        {
            RepoCategory repoCategory = new RepoCategory();
            ViewBag.category = repoCategory.GetCategoryDDl();
            if (id > 0)
            {
                RepoBrand repoBrand = new RepoBrand();
                var brand = repoBrand.GetBrandById(Convert.ToInt32(id));
                return View(brand);
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddBrand(ClsBrand model, HttpPostedFileBase postedfile)
        {
            try
            {
                RepoCategory repoCategory = new RepoCategory();
                ViewBag.category = repoCategory.GetCategoryDDl();
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (model.Id == 0 && postedfile == null)
                {
                    ModelState.AddModelError("PostedFile", "Please upload brand Image.");
                    return View(model);
                }
                if (postedfile != null)
                {

                    // save image file
                    RepoCommon common = new RepoCommon();
                    model.ImagePath = common.SaveImage(postedfile);
                }

                RepoBrand repoBrand = new RepoBrand();
                var isSaved = repoBrand.AddOrUpdateBrand(model);

                if (isSaved)
                {
                    TempData["SuccessMessage"] = "Brand add or update successfully!!";
                    return RedirectToAction("Brand", "Admin");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failled to add or update brand";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }
        }

        public ActionResult DeleteBrand(int id)
        {
            try
            {
                RepoBrand repoBrand = new RepoBrand();
                var isDelete = repoBrand.DeleteBrandById(id);
                return Json(new ResultModel
                {
                    ResultFlag = isDelete,
                    Data = null,
                    Message = isDelete ? "Brand deleted successfully!!" : "Failled to delete brand"
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

        #region Banner

        public ActionResult Banner()
        {
            return View();
        }
        public ActionResult BannerPartialView()
        {
            return View();
        }
        public ActionResult LoadBannerData()
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
                RepoBanner repoBanner = new RepoBanner();
                var banners = repoBanner.GetAllBanners(model, out recordsTotal);

                return Json(new { data = banners, draw = draw, recordsFiltered = banners.Count(), recordsTotal = recordsTotal }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = "", draw = Request.Form.GetValues("draw").FirstOrDefault(), recordsFiltered = 0, recordsTotal = 0, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddBanner(int? id)
        {
            if (id > 0)
            {
                RepoBanner repoBanner = new RepoBanner();
                var banner = repoBanner.GetBannerById(Convert.ToInt32(id));
                return View(banner);
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddBanner(ClsBanner model, HttpPostedFileBase postedfile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return View(model);
                }

                if (model.Id == 0 && postedfile == null)
                {
                    ModelState.AddModelError("PostedFile", "Please upload banner Image.");
                    return View(model);
                }
                if (postedfile != null)
                {

                    // save image file
                    RepoCommon common = new RepoCommon();
                    model.ImagePath = common.SaveImage(postedfile);
                }

                RepoBanner repoBanner = new RepoBanner();
                var isSaved = repoBanner.AddOrUpdateBanner(model);

                if (isSaved)
                {
                    TempData["SuccessMessage"] = "Banner add or update successfully!!";
                    return RedirectToAction("Banner", "Admin");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failled to add or update banner";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }
        }

        public ActionResult DeleteBanner(int id)
        {

            try
            {
                RepoBanner repoBanner = new RepoBanner();
                var isDelete = repoBanner.DeleteBannerById(id);
                return Json(new ResultModel
                {
                    ResultFlag = isDelete,
                    Data = null,
                    Message = isDelete ? "Banner deleted successfully!!" : "Failled to delete banner."
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

        #region Dimension

        public ActionResult Dimension()
        {
            return View();
        }
        public ActionResult DimensionPartialView()
        {
            return View();
        }
        public ActionResult LoadDimensionData()
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
                RepoDimension dimension = new RepoDimension();
                var dimensionsList = dimension.GetAllDimensions(model, out recordsTotal);

                return Json(new { data = dimensionsList, draw = draw, recordsFiltered = dimensionsList.Count(), recordsTotal = recordsTotal }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = "", draw = Request.Form.GetValues("draw").FirstOrDefault(), recordsFiltered = 0, recordsTotal = 0, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AddDimension(int? id)
        {
            try
            {
                RepoDimension rsDimension = new RepoDimension();
                ViewBag.Dimension = rsDimension.GetDimensionDDl();

                if (id > 0)
                {
                    RepoDimension rsCategory = new RepoDimension();
                    var dimension = rsCategory.GetDimensionById(Convert.ToInt32(id));
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
        public ActionResult AddDimension(DimensionModel model)
        {
            try
            {
                RepoDimension rsDimension = new RepoDimension();
                ViewBag.Dimension = rsDimension.GetDimensionDDl();
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var isSaved = rsDimension.AddOrUpdateDimension(model);

                if (isSaved)
                {
                    TempData["SuccessMessage"] = "Dimension add or update successfully!!";
                    return RedirectToAction("Dimension", "Admin");
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

        public ActionResult DeleteDimension(int id)
        {
            try
            {
                RepoDimension rsDimension = new RepoDimension();
                var isDelete = rsDimension.DeleteDimensionById(id);

                return Json(new ResultModel
                {
                    ResultFlag = isDelete,
                    Data = null,
                    Message = isDelete ? "Dimension deleted successfully!!" : "Failled to delete dimension"
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

        #region Weight

        public ActionResult Weight()
        {
            return View();
        }
        public ActionResult WeightPartialView()
        {
            try
            {
                RepoWeightMaster rsCategory = new RepoWeightMaster();
                var weight = rsCategory.GetAllWeight();
                return View(weight);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(new List<DimensionMaster>());
            }
        }
        public ActionResult AddWeight(int? id)
        {
            try
            {
                RepoWeightMaster rsCategory = new RepoWeightMaster();
                ViewBag.Dimension = rsCategory.GetweightDDl();

                if (id > 0)
                {
                    var dimension = rsCategory.GetWeightById(id.Value);
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
        public ActionResult AddWeight(WeightModel model)
        {
            try
            {
                RepoWeightMaster rsDimension = new RepoWeightMaster();
                ViewBag.Dimension = rsDimension.GetweightDDl();
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var isSaved = rsDimension.AddOrUpdateWeight(model);

                if (isSaved)
                {
                    TempData["SuccessMessage"] = "Weight add or update successfully!!";
                    return RedirectToAction("Weight", "Admin");
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

        public ActionResult DeleteWeight(int id)
        {
            try
            {
                RepoWeightMaster rsDimension = new RepoWeightMaster();
                var isDelete = rsDimension.DeleteWeightById(id);

                return Json(new ResultModel
                {
                    ResultFlag = isDelete,
                    Data = null,
                    Message = isDelete ? "Weight deleted successfully!!" : "Failled to delete weight."
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

        #region Color

        public ActionResult Color()
        {
            return View();
        }
        public ActionResult ColorPartialView()
        {
            try
            {
                RepoColor rsCategory = new RepoColor();
                var weight = rsCategory.GetAllColor();
                return View(weight);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(new List<DimensionMaster>());
            }
        }
        public ActionResult AddColor(int? id)
        {
            try
            {
                RepoColor rsCategory = new RepoColor();
                ViewBag.Dimension = rsCategory.GetColorDDl();

                if (id > 0)
                {
                    var dimension = rsCategory.GetColorById(id.Value);
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
        public ActionResult AddColor(ColorModel model)
        {
            try
            {
                RepoColor rsDimension = new RepoColor();
                ViewBag.Dimension = rsDimension.GetAllColor();
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var isSaved = rsDimension.AddOrUpdateColor(model);

                if (isSaved)
                {
                    TempData["SuccessMessage"] = "Color add or update successfully!!";
                    return RedirectToAction("Color", "Admin");
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

        public ActionResult DeleteColor(int id)
        {
            try
            {
                RepoColor rsDimension = new RepoColor();
                var isDelete = rsDimension.DeleteColorById(id);

                return Json(new ResultModel
                {
                    ResultFlag = isDelete,
                    Data = null,
                    Message = isDelete ? "Color deleted successfully!!" : "Failled to delete color."
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

        #region Product

        public ActionResult Products()
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
               
                RepoProductDetails productDetails = new RepoProductDetails();
                var productList = productDetails.GetAllUnApprovedProducts(model, out recordsTotal);

                return Json(new { data = productList, draw = draw, recordsFiltered = productList.Count(), recordsTotal = recordsTotal }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = "", draw = Request.Form.GetValues("draw").FirstOrDefault(), recordsFiltered = 0, recordsTotal = 0, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #region Product Details

        #region ProductImage
        [HttpPost]        
        public ActionResult ProductImage(int productId)
        {
            try
            {
                RepoProductDetails productDetails = new RepoProductDetails();
                var images = productDetails.GetProductImagesByProductId(productId);
                return View(images);
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View(new List<string>());
        }
        #endregion

        #region Product BasicInfo
        [HttpPost]
        public ActionResult ProductBasicInfo(int productId)
        {
            try
            {
                RepoProductDetails productDetail = new RepoProductDetails();
                var basicInfo = productDetail.GetProductBasicInfoByProductId(productId);
                return View(basicInfo);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View();
        }
        #endregion

        #region Product Details
        [HttpPost]
        public ActionResult ProductDetails(int productId)
        {
            try
            {
                RepoProductDetails productDetail = new RepoProductDetails();
                var detailInfo = productDetail.GetProductDetailByProductId(productId);
                return View(detailInfo);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View();
        }
        #endregion

        #region Product Dimension
        [HttpPost]
        public ActionResult ProductDimension(int productId)
        {
            try
            {
                RepoProductDetails productDetail = new RepoProductDetails();
                var detailInfo = productDetail.GetProductDimensionByProductId(productId);
                return View(detailInfo);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View();
        }
        #endregion
        #endregion

        #region  Approved Product
        public ActionResult ApprovedProduct(int productId)
        {
            try
            {
                RepoProductDetails productDetails = new RepoProductDetails();
                var isApproved = productDetails.ApprovedProductByAdmin(productId);
                return Json(new ResultModel
                {
                    ResultFlag = isApproved,
                    Data = null,
                    Message = isApproved ? "Product approved successfully!!" : "Failled to approved product."
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

        #region  Approved Product
        public ActionResult RejectProduct(int productId,string rejectRegion)
        {
            try
            {
                RepoProductDetails productDetails = new RepoProductDetails();
                var isApproved = productDetails.RejectProductByAdmin(productId, rejectRegion);
                return Json(new ResultModel
                {
                    ResultFlag = isApproved,
                    Data = null,
                    Message = isApproved ? "Product reject successfully!!" : "Failled to reject product."
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


        #endregion

        #region Vendors
        public ActionResult Vendor()
        {
            return View();
        }
        public ActionResult VendorPartialView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetVendorsData()
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

                RepoVendor repoVendor = new RepoVendor();
                var vendors = repoVendor.GetAllVendors(model, out recordsTotal);

                return Json(new { data = vendors, draw = draw, recordsFiltered = vendors.Count(), recordsTotal = recordsTotal }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = "", draw = Request.Form.GetValues("draw").FirstOrDefault(), recordsFiltered = 0, recordsTotal = 0, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult RegisterVendor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterVendor(Customer model)
        {
            return View(model);
        }
        #endregion

        #region Customers
        public ActionResult Customer()
        {
            return View();
        }
        public ActionResult CustomerPartialView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetCustomerData()
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

                RepoUser repoUser = new RepoUser();
                var users = repoUser.GetAllCustomers(model, UserRoles.Customer, out recordsTotal);

                return Json(new { data = users, draw = draw, recordsFiltered = users.Count(), recordsTotal = recordsTotal }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = "", draw = Request.Form.GetValues("draw").FirstOrDefault(), recordsFiltered = 0, recordsTotal = 0, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}