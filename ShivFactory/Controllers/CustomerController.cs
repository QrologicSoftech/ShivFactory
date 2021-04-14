using ShivFactory.Business.Model;
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
        Utility util = new Utility();
        public ActionResult Profile()
        {
            //Basic Details

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
            //  var cartList = cart.GetUserCart(cooki.GetCookiesValue(CookieName.TempOrderId));
            return View();


        }

        [HttpPost]
        public ActionResult Orders(AddToCart model)
        {
            return View(model);
        }
        #endregion Orders

        #region Checkout
        public ActionResult Checkout()
        {
            RepoCheckout checkout = new RepoCheckout();
            var checkoutModel = checkout.GetCheckoutData();
            if (checkoutModel != null) {
                return View(checkoutModel);
            } else {
                return View();
            }
        }

        //[HttpPost]
        //public ActionResult Checkout()
        //{ 

        //}

        #endregion

        #region Address
        public ActionResult Address()
        {
            RepoDeliveryAddress repoaddress = new RepoDeliveryAddress();
            var address = repoaddress.GetUserAddress(); 
            return View(address); 
        }
        #endregion


    }

}