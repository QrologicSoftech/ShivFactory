using ShivFactory.Business.Models.Other;
using System;
using System.Web;

namespace ShivFactory.Business.Repository
{
    public class RepoCookie
    {
        public void AddCookiesValues(LogInResponse model)
        {
            HttpCookie username = new HttpCookie(CookieName.UserName,model.UserName);
            username.Expires = DateTime.Now.AddMonths(1);
            HttpContext.Current.Response.Cookies.Add(username);

            HttpCookie userid = new HttpCookie(CookieName.UserId, model.UserId);
            userid.Expires = DateTime.Now.AddMonths(1);
            HttpContext.Current.Response.Cookies.Add(userid);

            HttpCookie role = new HttpCookie(CookieName.Role, model.Role);
            role.Expires = DateTime.Now.AddMonths(1);
            HttpContext.Current.Response.Cookies.Add(role);

            HttpCookie firstName = new HttpCookie(CookieName.FirstName, model.FirstName);
            firstName.Expires = DateTime.Now.AddMonths(1);
            HttpContext.Current.Response.Cookies.Add(firstName);

            HttpCookie lastName = new HttpCookie(CookieName.LastName, model.LastName);
            lastName.Expires = DateTime.Now.AddMonths(1);
            HttpContext.Current.Response.Cookies.Add(lastName);

            HttpCookie emailId = new HttpCookie(CookieName.EmailId, model.EmailId);
            emailId.Expires = DateTime.Now.AddMonths(1);
            HttpContext.Current.Response.Cookies.Add(emailId);

            HttpCookie mobile = new HttpCookie(CookieName.Mobile, model.Mobile);
            mobile.Expires = DateTime.Now.AddMonths(1);
            HttpContext.Current.Response.Cookies.Add(mobile);
        }
        public void AddCookiesValue(string cookieName, string value)
        {
            HttpCookie cookie = new HttpCookie(cookieName, value);
            cookie.Expires = DateTime.Now.AddMonths(1);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public string GetCookiesValue(string cookieName)
        {
            var val = HttpContext.Current.Request.Cookies[cookieName].Value;
            return val;
        }

    }

    public class RepoSession
    {
        public void AddSessionValues()
        {
            HttpContext.Current.Session["UserName"] = HttpContext.Current.Request.Cookies["UserName"].Value;
            HttpContext.Current.Session["UserId"] = HttpContext.Current.Request.Cookies["UserId"].Value;
            HttpContext.Current.Session["Role"] = HttpContext.Current.Request.Cookies["Role"].Value;
            HttpContext.Current.Session["FirstName"] = HttpContext.Current.Request.Cookies["FirstName"].Value;
            HttpContext.Current.Session["LastName"] = HttpContext.Current.Request.Cookies["LastName"].Value;
            HttpContext.Current.Session["EmailId"] = HttpContext.Current.Request.Cookies["EmailId"].Value;
            HttpContext.Current.Session["Mobile"] = HttpContext.Current.Request.Cookies["Mobile"].Value;
            HttpContext.Current.Session.Timeout = 60;
        }
        public void AddSessionValue(string sessionName, string value)
        {
            HttpContext.Current.Session[sessionName] = value;
            HttpContext.Current.Session.Timeout = 60;
        }
        public string GetSessionValue(string sessionName)
        {
            var val = HttpContext.Current.Session[sessionName].ToString();
            return val;
        }

    }
}
