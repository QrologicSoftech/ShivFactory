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
        #endregion
        public bool AddToCart(AddToCart model)
        {
            Utility utility = new Utility();
            try
            {
                // chek user has order or not 
                var tmpOrder = db.TempOrders.Where(a => a.UserId == utility.GetCurrentUserId()).FirstOrDefault();
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

                    RepoCookie cooki = new RepoCookie();
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

                    RepoCookie cooki = new RepoCookie();
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
            return userCart; 
        }
    }
}
