using DataLibrary.DL;
using ShivFactory.Business.Model;
using ShivFactory.Business.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShivFactory.Business.Repository
{
    public class RepoVendor
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        #endregion

        #region Get VendorId By UserId
        public int GetVendorIdByUserId(string userId)
        {
            var vendor = db.Vendors.Where(a => a.UserId == userId).AsNoTracking().FirstOrDefault();
            if (vendor != null)
            {
                return vendor.VendorId;
            }
            return 0;
        }

        #endregion

        #region Add Or Update VendorDetails
        public bool AddOrUpdateVendor(Vendor model)
        {
            var vendor = db.Vendors.Where(a => a.UserId == model.UserId).FirstOrDefault();
            if (vendor != null)
            {
                vendor.FirmName = model.FirmName;
                vendor.GSTIN = model.GSTIN;
                vendor.PanNo = model.PanNo;
                vendor.City = model.City;
                vendor.State = model.State;
            }
            else
            {
                db.Vendors.Add(model);
            }
            return db.SaveChanges() > 0;
        }
        #endregion

        #region Add Or Update VendorDetails
        public bool AddVendorBankDetails(DataLibrary.DL.VendorBankDetail model)
        {
            var vendorbankdetails = db.VendorBankDetails.Where(a => a.UserID == model.UserID).FirstOrDefault();
            if (vendorbankdetails != null)
            {
            }
            else
            {
                db.VendorBankDetails.Add(model);
            }
            return db.SaveChanges() > 0;
        }
        #endregion

        #region Get VendorDetails By UserId
        public Vendor GetVendorDetailsByUserId(string userId)
        {
            var vendor = db.Vendors.Where(a => a.UserId == userId).FirstOrDefault();
                return vendor;
         
          
        }

        #endregion

    }
}
