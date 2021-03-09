using DataLibrary.DL;
using ShivFactory.Business.Models.Other;
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
        public UserProfile GetUserDetailsBYUserId(string userId)
        {
            UserProfile userProfileResonse = new UserProfile();
            RepoCookie repoCookie = new RepoCookie();
            var role = repoCookie.GetCookiesValue(CookieName.Role);
            var user = db.UserDetails.Where(a => a.UserId == userId).FirstOrDefault();
            if (user != null)
            {
                userProfileResonse = new UserProfile
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address,
                    Email = user.Email,
                    Mobile = user.Mobile,
                    Gender = user.Gender,
                    UserImage = user.UserImage,
                };
            }
            if (role == UserRoles.Vendor)
            {
                var vendor = db.Vendors.Where(vndr => vndr.UserId == userId).FirstOrDefault();
                if (vendor != null)
                {
                    userProfileResonse.FirmName = vendor.FirmName;
                    userProfileResonse.GSTIN = vendor.GSTIN;
                    userProfileResonse.FullAddress = vendor.FullAddress;
                    userProfileResonse.City = vendor.City;
                    userProfileResonse.State = vendor.State;
                    userProfileResonse.PanNo = vendor.PanNo;
                    userProfileResonse.AddressProofImg = vendor.AddressProofImg;
                }

                var vendorbank = db.VendorBankDetails.Where(a => a.UserID == vendor.VendorId).FirstOrDefault();
                if (vendorbank != null)
                {
                    userProfileResonse.AccountHolderName = vendorbank.AccountHolderName;
                    userProfileResonse.AccountNumber = vendorbank.AccountNumber;
                    userProfileResonse.BankName = vendorbank.BankName;
                    userProfileResonse.IFSCCode = vendorbank.IFSCCode;
                    userProfileResonse.Branch = vendorbank.Branch;
                  //  userProfileResonse.IsActiveBank = false; //vendorbank.IsActive; 
                }
            }
            return userProfileResonse;
        }
        #endregion
    }
}
