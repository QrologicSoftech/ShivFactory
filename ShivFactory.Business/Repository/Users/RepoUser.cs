using DataLibrary.DL;
using ShivFactory.Business.Repository.ChangeProfileImage;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        #region User Is Delete
        public bool UserIsDelete(string userId)
        {
            var user = db.UserDetails.Where(a => a.UserId == userId && a.IsDelete==true).AsNoTracking().FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            return false;
        }
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

        #region update UserImage by UserId
        public bool UpdateUserImage(UserProfileImage model)
        {
            var user = db.UserDetails.Where(a => a.UserId == model.UserId).FirstOrDefault();
            if (user != null)
            {
                RepoCommon rc = new RepoCommon();
                bool delete = rc.deletefile(user.UserImage);
                user.UserImage = model.ImagePath; 
                user.LastUpdate = DateTime.Now;
            }
            
            return db.SaveChanges() > 0;
        }
        #endregion

        #region GetUserImage
        public string GetUserImage(string userId)
        {
            var image = db.UserDetails.Where(a => a.UserId == userId).Select(a => a.UserImage).FirstOrDefault();
            RepoCommon repoCommon = new RepoCommon();
            return  repoCommon.checkfile(image);
           
        }
        #endregion


        #region Get UserDetails by EmailId
        public UserDetail GetUserDetailsBYEmailId(string emailID)
        {
            var user = db.UserDetails.Where(a => a.Email == emailID).FirstOrDefault();
            return user;
        }
        #endregion

        #region update UserPassword by UserId
        public bool UpdateUserPassword(string userID,string password)
        {
            var user = db.UserDetails.Where(a => a.UserId == userID).FirstOrDefault();
            if (user != null)
            {
                user.Password = password; 
                user.LastUpdate = DateTime.Now;
            }
            return db.SaveChanges() > 0;
        }
        #endregion
    }
}
