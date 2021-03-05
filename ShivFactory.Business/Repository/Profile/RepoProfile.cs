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
        public UserProfile GetUserDetailsBYUserId(string userId)
        {
            var user = db.UserDetails.Where(a => a.UserId == userId).FirstOrDefault();
            var vendor = db.Vendors.Where(vndr => vndr.UserId == userId).FirstOrDefault();
            //var vendorbank = db.VendorBankDetails.Where(vndrbnk=> vndrbnk.UserID ==userId).FirstOrDefault();
            var userProfileResonse = new UserProfile {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Email = user.Email,
                Mobile = user.Mobile,
                Gender = user.Gender,
                UserImage = user.UserImage,

                FirmName = vendor.FirmName,
                GSTIN = vendor.GSTIN,
                FullAddress = vendor.FullAddress,
                City = vendor.City,
                State = vendor.State,
                PanNo = vendor.PanNo,
                AddressProofImg = vendor.AddressProofImg
                //,


                //AccountHolderName = vendorbank.AccountHolderName,
                //AccountNumber = vendorbank.AccountNumber,
                //BankName = vendorbank.BankName,
                //IFSCCode = vendorbank.IFSCCode,
                //Branch = vendorbank.Branch,
            };
          
            return userProfileResonse;
        }
        #endregion
    }
}
