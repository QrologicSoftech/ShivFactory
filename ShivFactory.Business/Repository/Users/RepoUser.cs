using DataLibrary.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
    public class RepoUser
    {
        #region Entity
        ShivFactoryEntities db = new ShivFactoryEntities();
        #endregion

        #region Add Or Update UserDetails
        public bool AddOrUpdateUserDetails(UserDetail model)
        {
            var user = db.UserDetails.Where(a => a.UserId == model.UserId).FirstOrDefault();
            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Address = model.Address;
                user.Gender = model.Gender;
                user.Mobile = model.Mobile;
                user.Email = model.Email;
                user.LastUpdate = DateTime.Now;
            }
            else
            {
                db.UserDetails.Add(model);
            }
            return db.SaveChanges() > 0;
        }
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
