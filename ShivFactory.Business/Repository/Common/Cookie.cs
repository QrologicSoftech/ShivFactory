using ShivFactory.Business.Models.Other;
using System;
using System.Web;
using System.Web.Mvc;

namespace ShivFactory.Business.Repository
{
    public class RepoCookie
    {
        public void AddCookiesValues(LogInResponse model)
        {
            HttpCookie username = new HttpCookie(CookieName.UserName, model.UserName);
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
            var val = ""; 
            try
            {
             val = HttpContext.Current.Request.Cookies[cookieName].Value;
            }
            catch (Exception e)
            {
                val = "" ; 
            }
            return val;
        }

        public void ClearCookiesValues()
        {
            HttpCookie username = new HttpCookie(CookieName.UserName, null);
            username.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(username);

            HttpCookie userid = new HttpCookie(CookieName.UserId, null);
            userid.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(userid);

            HttpCookie role = new HttpCookie(CookieName.Role, null);
            role.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(role);

            HttpCookie firstName = new HttpCookie(CookieName.FirstName, null);
            firstName.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(firstName);

            HttpCookie lastName = new HttpCookie(CookieName.LastName, null);
            lastName.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(lastName);

            HttpCookie emailId = new HttpCookie(CookieName.EmailId, null);
            emailId.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(emailId);

            HttpCookie mobile = new HttpCookie(CookieName.Mobile, null);
            mobile.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(mobile);
        }

    }

    public class RepoSession
    {
        public void AddSessionValues()
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.Cookies[CookieName.UserName].Value.ToString()))
            {
                ActionExecutingContext filterContextORG = new ActionExecutingContext();
                filterContextORG.Result = new RedirectResult("~/Account/LogIn");
                return;
            }

            HttpContext.Current.Session[CookieName.UserName] = HttpContext.Current.Request.Cookies[CookieName.UserName].Value;
            HttpContext.Current.Session[CookieName.UserId] = HttpContext.Current.Request.Cookies[CookieName.UserId].Value;
            HttpContext.Current.Session[CookieName.Role] = HttpContext.Current.Request.Cookies[CookieName.Role].Value;
            HttpContext.Current.Session[CookieName.FirstName] = HttpContext.Current.Request.Cookies[CookieName.FirstName].Value;
            HttpContext.Current.Session[CookieName.LastName] = HttpContext.Current.Request.Cookies[CookieName.LastName].Value;
            HttpContext.Current.Session[CookieName.EmailId] = HttpContext.Current.Request.Cookies[CookieName.EmailId].Value;
            HttpContext.Current.Session[CookieName.Mobile] = HttpContext.Current.Request.Cookies[CookieName.Mobile].Value;
            HttpContext.Current.Session.Timeout = 60;
        }
        public void AddSessionValue(string sessionName, string value)
        {
            HttpContext.Current.Session[sessionName] = value;
            HttpContext.Current.Session.Timeout = 60;
        }
        public string GetSessionValue(string sessionName)
        {
            if (HttpContext.Current.Session[CookieName.UserName]==null)
            {
                AddSessionValues();
            }            
            var val = HttpContext.Current.Session[sessionName].ToString();
            return val;
        }

    }
}
