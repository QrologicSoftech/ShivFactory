using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ShivFactory.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
          return View();
        }

        public ActionResult dashBoard()
        {
            return View();
        }
        public ActionResult NavPartialView()
        {
            return View();
        }


    }
}