using DataLibrary.DL;
using ShivFactory.Business.Model;
using ShivFactory.Business.Model.Common;
using ShivFactory.Business.Models;
using ShivFactory.Business.Models.Other;
using ShivFactory.Business.Repository.ChangeProfileImage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
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
            var user = db.UserDetails.Where(a => a.UserId == userId && a.IsDelete == true).AsNoTracking().FirstOrDefault();
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
            return repoCommon.checkfile(image);

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
        public bool UpdateUserPassword(string userID, string password)
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

        #region Get All Customers
        public List<CustomerResponse> GetAllCustomers(PaginationRequest model, string role, out int totalRecords)
        {
            var customers = new List<CustomerResponse>();
            totalRecords = 0;
            RepoCommon common = new RepoCommon();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Action", "GetAllCustomers"));
            parameters.Add(new SqlParameter("@SearchText", model.searchText));
            parameters.Add(new SqlParameter("@Skip", model.Skip));
            parameters.Add(new SqlParameter("@Take", model.PageSize));
            parameters.Add(new SqlParameter("@OrderColumn", model.SortColumn));
            parameters.Add(new SqlParameter("@OrderDir", model.SortDirection));
            parameters.Add(new SqlParameter("@Role", role));

            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnectionString, "ManageUsers", parameters.ToArray());
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                totalRecords = ds.Tables[0].Rows[0]["TotalRow"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["TotalRow"].ToString()) : 0;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    customers.Add(new CustomerResponse()
                    {
                        SrNo = row["SrNo"] != DBNull.Value ? Convert.ToInt32(row["SrNo"]) : 0,
                        Id = row["Id"] != DBNull.Value ? row["Id"].ToString() : "",
                        ImagePath = row["ImagePath"] != DBNull.Value ? common.checkfile(row["ImagePath"].ToString()) : common.checkfile(""),
                        FirstName = row["FirstName"] != DBNull.Value ? row["FirstName"].ToString() : "",
                        LastName = row["LastName"] != DBNull.Value ? row["LastName"].ToString() : "",
                        Gender = row["Gender"] != DBNull.Value ? row["Gender"].ToString() : "",
                        UserName = row["UserName"] != DBNull.Value ? row["UserName"].ToString() : "",
                        Address = row["Address"] != DBNull.Value ? row["Address"].ToString() : "",
                        Status = row["Status"] != DBNull.Value ? row["Status"].ToString() : "",
                        AddDate = row["AddDate"] != DBNull.Value ? Convert.ToDateTime(row["AddDate"]).ToString("dd/MM/yyyy") : ""
                    });
                }
            }
            return customers;
        }

        #endregion

        #region BlockUser By UserId
        public bool BlockUserByUserId(string userId)
        {
            var user = db.UserDetails.Where(a => a.UserId == userId).FirstOrDefault();
            if (user != null)
            {
                user.IsDelete = user.IsDelete==true?false:true;
                return db.SaveChanges() > 0;
            }
            return false;
        }
        #endregion

        #region UpdateTempOrder

        public void UpdateTempOrder()
        {
            Utility utility = new Utility();
            string userId = utility.GetCurrentUserId();
            RepoCookie cookie = new RepoCookie();
            int tempOrderId = cookie.GetIntCookiesValue(CookieName.TempOrderId);

            var userDetail = db.UserDetails.Where(u => u.UserId == userId).FirstOrDefault();
            if (userDetail != null)
            {
                userDetail.TempOrderId = tempOrderId;
            }
            db.SaveChanges();

        }
        #endregion

        #region update UserEmail ID 
        public bool UpdateCurrentUserEmail(UserDetail model)
        {
            var user = db.UserDetails.Where(a => a.UserId == model.UserId).FirstOrDefault();
            if (user != null)
            {
                user.Email  = model.Email;
                user.LastUpdate = DateTime.Now;
            }
            return db.SaveChanges() > 0;
        }
        #endregion


        #region update UserMobile ID 
        public bool UpdateCurrentUserMobile(UserDetail model)
        {
                var user = db.UserDetails.Where(a => a.UserId == model.UserId).FirstOrDefault();
                if (user != null)
                {
                    user.Mobile = model.Mobile;
                    user.LastUpdate = DateTime.Now;
                }
                return db.SaveChanges() > 0;
        }
        #endregion
    }
}
