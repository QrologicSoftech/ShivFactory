using DataLibrary.DL;
using ShivFactory.Business.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
    public class RepoCheckout
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        Utility utility = new Utility();
        RepoUser user = new RepoUser();
        #endregion
        public CheckoutModel GetCheckoutData()
        {
            var checkOutModel = new CheckoutModel();
            string userid = utility.GetCurrentUserId();
            var userdetails = user.GetUserDetailsBYUserId(userid);
            var address = db.DeliveryAddresses.Where(row => row.UserId == userid).Select(row => new DeliveryAddress {
                ID = row.ID,
                Address1 = row.Address1,
                Address2 = row.Address2,
                Address3 = row.Address3,
                Phone = row.Phone,
                Country = row.Country,
                State = row.State,
                Pincode = row.Pincode,
                UserName = row.UserName,
                userDetailId = row.userDetailId??0

            }).FirstOrDefault();
            if (address != null)
            {
                checkOutModel.DeliveryAddress = new List<DeliveryAddress>();
                checkOutModel.DeliveryAddress.Add(address);
            }

            RepoCart cart = new RepoCart();
            var cartmodel = cart.GetCart();
            if (cartmodel != null)
            {
                checkOutModel.CartModel = new List<CartModel>(); 
                checkOutModel.CartModel.Add(cartmodel);
            }
            return checkOutModel;
        }
    }
}
