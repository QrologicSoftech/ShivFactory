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
            RepoCookie cooki = new RepoCookie();
            var cartList = cart.GetCart();
            return View(cartList);
        }
        #endregion 

        #region Orders
        public ActionResult Orders()
        {
            RepoCart cart = new RepoCart();
            RepoCookie cooki = new RepoCookie();
          //  var cartList = cart.GetUserCart(cooki.GetCookiesValue(CookieName.TempOrderId));
            return View();
            
            //        try {
            //            RepoCart cart = new RepoCart();
            //            RepoCookie cooki = new RepoCookie();
            //            // Initialization.  
            //            var search = "";// Request.Form.GetValues("search[value]")[0];
            //            var draw = "";// Request.Form.GetValues("draw").FirstOrDefault();
            //            var start = Request.Form.GetValues("start").FirstOrDefault();
            //            var length = Request.Form.GetValues("length").FirstOrDefault();
            //            //Find Order Column  
            //            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            //            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            //            // Prepair model  
            //            PaginationRequest model = new PaginationRequest()
            //            {
            //                searchText = search,
            //                Skip = start != null ? Convert.ToInt32(start) : 0,
            //                PageSize = length != null ? Convert.ToInt32(length) : 0,
            //                SortColumn = sortColumn,
            //                SortDirection = sortColumnDir
            //            };
            //            int recordsTotal = 0;
            //            string TempOrderID = cooki.GetCookiesValue(CookieName.TempOrderId);
            //            RepoCart repocart = new RepoCart();
            //            var orderList = repocart.GetUserCart(TempOrderID, model, out recordsTotal);
            //            return Json(new { data = orderList, draw = draw, recordsFiltered = orderList.Count, recordsTotal = recordsTotal }, JsonRequestBehavior.AllowGet);
            //        }
            //        catch (Exception ex)
            //        {
            //            return Json(new { data = "", draw = Request.Form.GetValues("draw").FirstOrDefault(), recordsFiltered = 0, recordsTotal = 0, error = ex.Message
            //}, JsonRequestBehavior.AllowGet);
            //        }
            
           }

        [HttpPost]
        public ActionResult Orders(AddToCart model)
        {
            return View(model);
        }
        #endregion Orders

    }

}