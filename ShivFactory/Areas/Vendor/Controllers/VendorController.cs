using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShivFactory.Areas.Vendor.Controllers
{
    [Authorize(Roles = "Vendor")]
    public class VendorController : Controller
    {
        // GET: Vendor/Vendor
        public ActionResult Index()
        {
            return View();
        }
    }
}