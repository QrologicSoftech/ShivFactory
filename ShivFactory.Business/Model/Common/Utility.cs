using ShivFactory.Business.Models.Other;
using ShivFactory.Business.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Model.Common
{
    public class Utility
    {
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
