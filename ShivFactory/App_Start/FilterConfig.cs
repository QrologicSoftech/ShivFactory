using System.Web;
using System.Web.Mvc;

namespace ShivFactory
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
        //public class UserSessionActionFilter : ActionFilterAttribute, IActionFilter
        //{
        //    public override void OnActionExecuting(ActionExecutingContext filterContextORG)
        //    {
        //        HttpContext ctx = HttpContext.Current;
        //        if (HttpContext.Current.Session["User"] == null)
        //        {
        //            /// this handles session when data is requested through Ajax json
        //            if (filterContextORG.HttpContext.Request.IsAjaxRequest())
        //            {
        //                JsonResult result = new JsonResult { Data = "Session Timeout!" };
        //                filterContextORG.Result = result;
        //            }
        //            else
        //            {
        //                /// If session is expired then redirected to logout page which further redirect to login page. 
        //                filterContextORG.Result = new RedirectResult("~/Main/Home/Logout");
        //                return;
        //            }
        //        }
        //    }
        //}
    }
}
