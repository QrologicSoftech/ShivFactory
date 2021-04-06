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


        #region cart

        public ActionResult ShowCart()
        {
            RepoCart cart = new RepoCart();
            var cartList = cart.GetCart();
            TempData.Peek("SuccessMessage");
            TempData.Peek("ErrorMessage");
            return View(cartList);
        }

        public ActionResult DeleteCartItem(int? Id)
        {
            try
            {
                if (Id > 0)
                {
                    RepoCart cart = new RepoCart();
                    var retval = cart.DeleteCartItemById((int)Id);
                    TempData["SuccessMessage"] = "Item removed successfully from Cart";
                    return RedirectToAction("ShowCart", "Customer");
                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message.ToString();
             
            }
          return  RedirectToAction("ShowCart", "Customer");

        }

        public ActionResult UpdateCart(UpdateCart model)
        {
            try
            {
                RepoCart cart = new RepoCart();
                bool IsAddToCart = cart.UpdateCart(model);
                return Json(new ResultModel
                {
                    ResultFlag = IsAddToCart,
                    Data = model,
                    Message = IsAddToCart == true ? "Cart updated successfully" : " Unable to Add Item in cart "
                }, JsonRequestBehavior.AllowGet); ;
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


        #region WishList
        public ActionResult Wishlist()
        {
            return View();
        }
        #endregion
    }

}