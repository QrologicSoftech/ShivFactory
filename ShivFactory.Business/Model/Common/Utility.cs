using ShivFactory.Business.Models.Other;
using ShivFactory.Business.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ShivFactory.Business.Model.Common
{
    public class Utility
    {
        #region Is UserLogin
        public bool IsUserLogIn()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName.UserName];
            if (cookie==null)
            // if (string.IsNullOrEmpty(HttpContext.Current.Request.Cookies[CookieName.UserName].Value.ToString()))
            {
                return false;
            }
            return true;
        }
        #endregion

        #region GetCurrent UserId
        public string GetCurrentUserId()
        {
            RepoSession rps = new RepoSession();
            return rps.GetSessionValue(CookieName.UserId);
        }
        #endregion

        #region Get Current UserRole
        public string GetCurrentUserRole()
        {
            RepoSession rps = new RepoSession();
            return rps.GetSessionValue(CookieName.Role);
        }
        #endregion
    }
}
