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

        public ActionResult UserProfile()
        {
            UserProfile model = new UserProfile();
            Utility util = new Utility();
            model.UserId = util.GetCurrentUserId();
            RepoCommon rc = new RepoCommon();
            RepoUser ru = new RepoUser();
            UserDetail userDetail = ru.GetUserDetailsBYUserId(model.UserId);
            return View(); 
        }

        [HttpPost]
        public ActionResult UserProfile(UserProfile model)
        {
            return View();
        }
    }
}