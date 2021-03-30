﻿using DataLibrary.DL;
using ShivFactory.Business.Model;
using ShivFactory.Business.Model.Common;
using ShivFactory.Business.Repository;
using ShivFactory.Business.Repository.ChangeProfileImage;
using ShivFactory.Business.Repository.Website;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace ShivFactory.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        #region Get Index Data
        public ActionResult GetIndexData()
        {
            try
            {
                RepoHomePage home = new RepoHomePage();
                var index = home.GetHomePageData();
                return Json(new ResultModel
                {
                    ResultFlag = true,
                    Data = index,
                    Message = "Data found successfully!!"
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

        #region Change Profile Image
        public ActionResult ChangeProfileImage()
        {
            UserProfileImage model = new UserProfileImage();
            model.ReturnUrl = Request.UrlReferrer.AbsolutePath.ToString();
            Utility util = new Utility();

            model.UserId = util.GetCurrentUserId();
            RepoUser ru = new RepoUser();
            UserDetail userDetail = ru.GetUserDetailsBYUserId(model.UserId);
            if (userDetail != null)
            {
                model.ImagePath = userDetail.UserImage;

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeProfileImage(UserProfileImage model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                RepoCommon repoCommon = new RepoCommon();
                RepoUser repoUser = new RepoUser();
                // save new img
                model.ImagePath = repoCommon.SaveImage(model.PostedFile);
                bool update = repoUser.UpdateUserImage(model);
                if (update)
                {
                    TempData["SuccessMessage"] = "Profile successfully updated!!";
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    return View();
                }

                TempData["ErrorMessage"] = "Failled to update profile.";
                return View(model);
            }

        }
        #endregion

        #region UserProfile or AccountDetails
        public ActionResult GetCurrentUserDetails()
        {
            try
            {
                // Call for Vendor only 
                Utility util = new Utility();
                RepoProfile repoProfile = new RepoProfile();
                var userDetail = repoProfile.GetUserDetailsBYUserId(util.GetCurrentUserId());

                return Json(new ResultModel
                {
                    ResultFlag = userDetail != null ? true : false,
                    Data = userDetail,
                    Message = userDetail != null ? "User details find successfully!!" : "Failled to find user details!!"
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

        public ActionResult AccountDetails()
        {
            //Basic Details
            Utility util = new Utility();
            RepoProfile repoProfile = new RepoProfile();
            var userProfiledetails = repoProfile.GetUserDetailsBYUserId(util.GetCurrentUserId());
            // 
            return View(userProfiledetails);
        }

        public ActionResult DemoAccDetail()
        {
            return View();
        }
        public ActionResult SaveCurrentUserBasicDetails(clsUserBasicDetails model)
        {
            try
            {
                // Call for Customer and Vendor only 
                UserDetail userDetail = new UserDetail();

                Utility util = new Utility();
                RepoUser repoUser = new RepoUser();
                bool isUpdate = repoUser.AddOrUpdateUserDetails(new UserDetail()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    Gender = model.Gender,
                    UserId = util.GetCurrentUserId()
                });

                return Json(new ResultModel
                {
                    ResultFlag = isUpdate,
                    Data = "",
                    Message = isUpdate == true ? "User details Update successfully!!" : "Failled to Update user details!!"
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

        public ActionResult SaveCurrentVendorDetails(clsVendorBasicDetails model)
        {
            try
            {
                RepoCommon repoCommon = new RepoCommon();
                var imgpath = repoCommon.Base64ToImage(model.AddressProof);
                Vendor vendorDetail = new Vendor();
                Utility util = new Utility();
                RepoVendor repoUser = new RepoVendor();
                bool isUpdate = repoUser.AddOrUpdateVendor(new Vendor()
                {
                    FirmName = model.FirmName,
                    GSTIN = model.GSTIN,
                    PanNo = model.PanNo,
                    City = model.City,
                    State = model.State,
                    PIN = model.PIN,
                    FullAddress = model.FullAddress,
                    AddressProofImg = imgpath,
                    UserId = util.GetCurrentUserId()
                });

                return Json(new ResultModel
                {
                    ResultFlag = isUpdate,
                    Data = "",
                    Message = isUpdate == true ? "User details Update successfully!!" : "Failled to Update user details!!"
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
        public ActionResult UpdateVendorBankDetails(clsVendorBankDetails model)
        {
            try
            {
                RepoCommon repoCommon = new RepoCommon();
                var imgpath = repoCommon.Base64ToImage(model.BankProof);
                Vendor vendorDetail = new Vendor();
                Utility util = new Utility();
                RepoVendor repoVendor = new RepoVendor();
                int vendorID = repoVendor.GetVendorIdByUserId(util.GetCurrentUserId());
                bool isUpdate = repoVendor.AddVendorBankDetails(new DataLibrary.DL.VendorBankDetail()
                {
                    AccountHolderName = model.AccountHolderName,
                    AccountNumber = model.AccountNumber,
                    IFSCCode = model.IFSCCode,
                    BankName = model.BankName,
                    UserID = vendorID,
                    BankProofImg = imgpath
                });

                return Json(new ResultModel
                {
                    ResultFlag = isUpdate,
                    Data = "",
                    Message = isUpdate == true ? "User Bank details Update successfully!!" : "Failled to Update user Bank details!!"
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


        #region GetVarients Filter
        public ActionResult GetVarientsFilter(ProductListingPagination model)
        {
            try
            {
                RepoVarient varient = new RepoVarient();
                var varients = varient.GetVarientsFilter(model);

                return Json(new ResultModel
                {
                    ResultFlag = true,
                    Data = varients,
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

        #region Website
        public ActionResult WebMenuPartialview()
        {
            try
            {
                RepoMenu menu = new RepoMenu();
                var menues = menu.GetAllMenues();
                return View(menues);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View(new MenuResponse());
        }

        [HttpPost]
        public ActionResult ProductAutoComplete(string prefix)
        {
            try
            {
                RepoAutoComplete auto = new RepoAutoComplete();
                var succession = auto.ProductAutoComplete(prefix);
                return Json(new ResultModel
                {
                    ResultFlag = succession.Count > 0,
                    Data = succession,
                    Message = succession.Count > 0 ? "Products founds successfully!!" : "No records found."

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

        #region Website Listing
        public ActionResult ProductListing(int? id, int? subId, int? minId, string searchtext)
        {
            ViewBag.CategoryId = id;
            ViewBag.SubCategoryId = subId;
            ViewBag.MiniCategoryId = minId;
            return View();
        }
        public ActionResult GetProducts(ProductListingPagination model)
        {
            try
            {
                RepoListing repoListing = new RepoListing();
                List<ClsProduct> list = repoListing.GetallProductlist(model);
                System.Threading.Thread.Sleep(100);
                return Json(new ResultModel
                {
                    ResultFlag = list != null ? true : false,
                    Data = list,
                    Message = list != null ? "Product find successfully!!" : "Failled to find Product!!"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new ResultModel
                {
                    ResultFlag = false,
                    Data = null,
                    Message = e.Message.ToString()
                }, JsonRequestBehavior.AllowGet);
            }

        }


        #endregion

        #region ProductDetail
        public ActionResult ProductDetail(int productId, string VarientName, string VarientValue)
        {
            try
            {
                productId = productId > 0 ? productId : 0;

                RepoListing repoListing = new RepoListing();
                var productDetail = repoListing.GetProductDetail(productId, VarientName, VarientValue);
                return View(productDetail);
            }
            catch (Exception e)
            {
                return View();
            }

        }

        #endregion

        #region Cart
        public ActionResult AddToCart(AddToCart model)
        {
            try
            {
                RepoCart cart = new RepoCart();
                bool  IsAddToCart = cart.AddToCart(model);
                return Json(new ResultModel
                {
                    ResultFlag = IsAddToCart,
                    Data = model,
                    Message = "Add to cart successfully"
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

        public ActionResult ShowCart(string userID)
        {
            RepoCart cart = new RepoCart();
            var userCart = cart.GetUserCart(userID);
            return View(userCart); 
        }
        #endregion

    }
}