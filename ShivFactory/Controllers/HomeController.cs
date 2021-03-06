using DataLibrary.DL;
using ShivFactory.Business.Model;
using ShivFactory.Business.Model.Common;
using ShivFactory.Business.Repository;
using ShivFactory.Business.Repository.ChangeProfileImage;
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ChangeProfileImage()
        {
            UserProfileImage model = new UserProfileImage(); 
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
                TempData["SuccessMessage"] = "Profile Image Updated Successfully";
                return View();
            }
            
        }


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
                    ResultFlag = userDetail!=null? true: false,
                    Data = userDetail,
                    Message = userDetail!=null ? "User details find successfully!!": "Failled to find user details!!"
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
                Vendor vendorDetail = new Vendor();
                Utility util = new Utility();
                RepoVendor repoUser = new RepoVendor();
                bool isUpdate = repoUser.AddOrUpdateVendor(new Vendor()
                {
                    FirmName = model.FirmName,
                    GSTIN = model.GSTIN,
                    PanNo = model.PanNo,
                    City = model.City,
                    State = model.State
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

        public ActionResult AccDetailpage()
        {
            return View(); 
        }
        #endregion
    }


}