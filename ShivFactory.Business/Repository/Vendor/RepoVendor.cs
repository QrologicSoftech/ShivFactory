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
        public bool AddVendor(Vendor model)
        {
            db.Vendors.Add(model);
            return db.SaveChanges() > 0;
        }
        #endregion

    }
}
