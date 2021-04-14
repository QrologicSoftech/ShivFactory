using DataLibrary.DL;
using ShivFactory.Business.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
  public   class RepoDeliveryAddress
    {

        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        Utility utility = new Utility();
        RepoUser user = new RepoUser();
        #endregion
        public DeliveryAddress GetUserAddress()
        {
            DeliveryAddress address = new DeliveryAddress(); 
            string userid = utility.GetCurrentUserId();
            var addressList = db.DeliveryAddresses.Where(row => row.UserId == userid).ToList();
             address.addresseList = db.DeliveryAddresses.Where(row => row.UserId == userid).Select(row => new DeliveryAddress
               {
                ID = row.ID,
                Address1 = row.Address1,
                Address2 = row.Address2,
                Address3 = row.Address3,
                Phone = row.Phone,
                Country = row.Country,
                State = row.State,
                Pincode = row.Pincode,
                UserName = row.UserName,
                userDetailId = row.userDetailId ?? 0
            }).OrderBy(row => row.ID).ToList();

            return address;
        }
    }
}

