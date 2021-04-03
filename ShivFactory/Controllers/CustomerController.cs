using ShivFactory.Business.Model.Common;
using ShivFactory.Business.Models.Other;
using ShivFactory.Business.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ShivFactory.FilterConfig;

namespace ShivFactory.Controllers
{
    [Authorize(Roles = "Customer"), UserSessionActionFilter]
    public class CustomerController : Controller
    {
        #region Profile
        public ActionResult Profile()
        {
            //Basic Details
            Utility util = new Utility();
            RepoProfile repoProfile = new RepoProfile();
            var userProfiledetails = repoProfile.GetUserDetailsBYUserId(util.GetCurrentUserId());
            return View(userProfiledetails);
        }
        [HttpPost]
        public ActionResult Profile(UserProfile model)
        {
            return View(model);
        }
        #endregion 


        #region Orders
        public ActionResult Orders()
        {
            RepoCart cart = new RepoCart();
            RepoCookie cooki = new RepoCookie();
            var cartList = cart.GetUserCart(cooki.GetCookiesValue(CookieName.TempOrderId));
            return View(); //Json(new { data = cartList }, JsonRequestBehavior.AllowGet);
            // We shall send this by indexing 
            //return Json(new { data = cartList, draw = draw, recordsFiltered = pincodeList.Count(), recordsTotal = recordsTotal }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Orders(AddToCart model)
        {
            return View(model);
        }
        #endregion Orders

    }

}