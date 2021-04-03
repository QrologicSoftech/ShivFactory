using DataLibrary.DL;
using ShivFactory.Business.Model.Common;
using ShivFactory.Business.Models.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
   public class RepoCart
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        RepoCookie cooki = new RepoCookie();
        Utility utility = new Utility();
        #endregion
        public bool AddToCart(AddToCart model)
        {
      
            string userid = utility.GetCurrentUserId(); 
            try
            {
                // chek user has order or not 
                var tmpOrder = db.TempOrders.Where(a => a.UserId == userid).FirstOrDefault();
                if (tmpOrder != null) {
                    var temporderDetails = db.TempOrderDetails.Add(new TempOrderDetail()
                    {
                        productID = model.ProductID,
                        ProductName = model.ProductName,
                        productVarientID = model.ProductVarientID,
                        Price = Convert.ToDecimal(model.Price),
                        Brand = model.Brand,
                        Quantity = Convert.ToInt32(model.Quantity),
                        TempOrderID = tmpOrder.ID,
                        AddDate = DateTime.Now
                    });
                    int retVal = db.SaveChanges();

                    cooki.AddCookiesValue(CookieName.TempOrderId, tmpOrder.ID.ToString());

                } else {
                    var tempOrder = db.TempOrders.Add(new TempOrder()
                    {
                        UserId = utility.GetCurrentUserId(),
                    });

                    var temporderDetails = db.TempOrderDetails.Add(new TempOrderDetail()
                    {
                        productID = model.ProductID,
                        ProductName = model.ProductName,
                        productVarientID = model.ProductVarientID,
                        Price = Convert.ToDecimal(model.Price),
                        Brand = model.Brand,
                        Quantity = Convert.ToInt32(model.Quantity),
                        TempOrderID = tempOrder.ID,
                        AddDate = DateTime.Now
                    });
                    int retVal = db.SaveChanges();

                    cooki.AddCookiesValue(CookieName.TempOrderId, tempOrder.ID.ToString());
                }
                
             

          
                return true;
            }
            catch (Exception e)
            {
                return false; 
            }
        }

        public List<AddToCart> GetUserCart(string tempOrderId)
        {
            
            List<AddToCart> userCart = new List<AddToCart>();
            Utility utility = new Utility();
            var TempOrder = db.TempOrders.Where(a => a.ID == Convert.ToInt32(tempOrderId));
            if (TempOrder != null)
            {
                var tempOrderDetails = db.TempOrderDetails.Where(row => row.TempOrderID == Convert.ToInt32(tempOrderId));
                if (tempOrderDetails != null)
                { // dont use this use indexing 
                //foreach(var record in tempOrderDetails)
                //    {
                //        AddToCart cart = new AddToCart();
                //        cart.ProductID = record.productID!=null ?record.productID.Value : 0;
                //        cart.ProductName = record.ProductName;
                //        cart.Price = record.Price!=null ? record.Price.Value : 0;
                //        cart.Quantity = record.Quantity!=null ? record.Quantity.Value :0; 
                //        userCart.Add(cart);     
                //    }
                }
            }
                return userCart; 
        }
    }
}
