using DataLibrary.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
   public class RepoProfile
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        #endregion

        #region Get UserDetails by UserId
        public UserDetail GetUserDetailsBYUserId(string userId)
        {
            var user = db.UserDetails.Where(a => a.UserId == userId).FirstOrDefault();
            return user;
        }
        #endregion
    }
}
