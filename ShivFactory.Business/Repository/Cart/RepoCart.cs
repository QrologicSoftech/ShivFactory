using DataLibrary.DL;
using ShivFactory.Business.Model;
using ShivFactory.Business.Model.Common;
using ShivFactory.Business.Models;
using ShivFactory.Business.Models.Other;
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
    public class RepoCart
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        RepoCookie cooki = new RepoCookie();

        #endregion
        Utility utility = new Utility();

        public bool AddToCart(AddToCart model)
        {
            int tempOrderId = 0;
            try
            {
                var tempOrderTbl = new TempOrder();
                string tempid = cooki.GetStringCookiesValue(CookieName.TempOrderId);
                if (tempid == "")
                {
                    tempid = "0";
                }
                tempOrderId = Convert.ToInt32(tempid);
                tempOrderTbl = db.TempOrders.Where(row => row.ID == tempOrderId).FirstOrDefault();
                if (tempOrderId == 0 || tempOrderTbl == null)
                {
                    var tempOrder = db.TempOrders.Add(new TempOrder()
                    {
                        AddDate = DateTime.Now
                    });
                    db.SaveChanges();
                    tempOrderId = tempOrder.ID;
                    cooki.AddCookiesValue(CookieName.TempOrderId, tempOrderId.ToString());
                }

                var orderDetails = db.TempOrderDetails.Where(a => a.ProductVarientId == model.ProductVarientID && a.TempOrderID == tempOrderId).FirstOrDefault();
                if (orderDetails != null)
                {
                    orderDetails.Quantity = orderDetails.Quantity + model.Quantity; //orderDetails.Quantity ?? 0 + m
                    orderDetails.NetAmt = orderDetails.Quantity * model.Price;
                    db.SaveChanges();
                }
                else
                {
                    var temporderDetails = new TempOrderDetail()
                    {
                        ProductId = model.ProductID,
                        ProductName = model.ProductName,
                        ProductVarientId = model.ProductVarientID,
                        Price = model.Price,
                        Quantity = model.Quantity,
                        TempOrderID = tempOrderId,
                        AddDate = DateTime.Now,
                        NetAmt = model.Quantity * model.Price,
                        IsUserWishList = model.IsUserWishList,
                        VendorId = model.vendorId
                    };
                    db.TempOrderDetails.Add(temporderDetails);
                }

                db.SaveChanges();
                TotalCartAmount(tempOrderId);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateCart(UpdateCart model)
        {
            try
            {
                int tempOrderId = Convert.ToInt32(cooki.GetStringCookiesValue(CookieName.TempOrderId));
                if (tempOrderId == 0)
                {
                    var tempOrder = db.TempOrders.Add(new TempOrder()
                    {
                        AddDate = DateTime.Now
                    });
                    tempOrderId = tempOrder.ID;
                }

                var orderDetails = db.TempOrderDetails.Where(a => a.ID == model.TempOrderDetailId).FirstOrDefault();
                if (orderDetails != null)
                {
                    orderDetails.Quantity = model.Quantity;
                    orderDetails.NetAmt = orderDetails.Quantity * orderDetails.Price;
                }

                db.SaveChanges();
                TotalCartAmount(tempOrderId);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public int GetUserCartCount()
        {
            if (cooki.GetStringCookiesValue(CookieName.TempOrderId) != "")
            {
                int tempId = Convert.ToInt32(cooki.GetStringCookiesValue(CookieName.TempOrderId));

                var items = db.TempOrderDetails.Where(row => row.TempOrderID == tempId);
                return items.Count();
            }
            else
            {
                return 0;
            }
        }
        public CartModel GetCart()
        {
            string userid = utility.GetCurrentUserId();
            int tempId = cooki.GetIntCookiesValue(CookieName.TempOrderId);
            var data = db.TempOrders.Where(a => a.ID == tempId).Select(a => new CartModel
            {
                CartValue = a.NetAmt ?? 0
            }).FirstOrDefault();
            if (data != null)
            {
                data.CartItems = (from tod in db.TempOrderDetails
                                  join p in db.Products
                                  on tod.ProductId equals p.ProductId
                                  where tod.TempOrderID == tempId && (tod.IsUserWishList == false)
                                  select new AddToCart
                                  {
                                      ProductID = tod.ProductId != null ? tod.ProductId.Value : 0,
                                      ProductName = tod.ProductName,
                                      Price = tod.Price ?? 0,
                                      Quantity = tod.Quantity ?? 0,
                                      NetAmt = tod.NetAmt ?? 0,
                                      ProductVarientID = tod.ProductVarientId ?? 0,
                                      vendorId = tod.VendorId ?? 0,
                                      ID = tod.ID,
                                      ImagePath = p.MainImage
                                  }).AsNoTracking().ToList();
            }
            return data;
        }

        public CartModel GetWishlist()
        {
            var model = new CartModel();
            int tempId = cooki.GetIntCookiesValue(CookieName.TempOrderId);
            var data = db.TempOrders.Where(a => a.ID == tempId).Select(a => new CartModel
            {
                CartValue = a.NetAmt ?? 0
            }).FirstOrDefault();
            if (data != null)
            {
                var items = (from tod in db.TempOrderDetails
                             join p in db.Products
                             on tod.ProductId equals p.ProductId
                             where tod.TempOrderID == tempId && tod.IsUserWishList == true

                             select new AddToCart
                             {
                                 ProductID = tod.ProductId != null ? tod.ProductId.Value : 0,
                                 ProductName = tod.ProductName,
                                 Price = tod.Price ?? 0,
                                 Quantity = tod.Quantity ?? 0,
                                 NetAmt = tod.NetAmt ?? 0,
                                 ProductVarientID = tod.ProductVarientId ?? 0,
                                 vendorId = tod.VendorId ?? 0,
                                 ID = tod.ID,
                                 ImagePath = p.MainImage
                             }).AsNoTracking().ToList();
                data.CartItems = items;
            }
            return data;
        }


        public void TotalCartAmount(int tempOrderId)
        {
            var NetAmt = db.TempOrderDetails.Where(row => row.TempOrderID == tempOrderId && row.IsUserWishList == false).Sum(row => row.NetAmt);
            var tempOrder = db.TempOrders.Where(row => row.ID == tempOrderId).FirstOrDefault();
            if (tempOrder != null)
            {
                tempOrder.NetAmt = Convert.ToDecimal(NetAmt);
                tempOrder.UpdateDate = DateTime.Now;
            }
            db.SaveChanges();
        }

        public bool DeleteCartItemById(int rowID)
        {
            bool retval = false;
            int temporderID = 0;
            var tempOrderDetails = db.TempOrderDetails.Where(row => row.ID == rowID).FirstOrDefault();
            if (tempOrderDetails != null)
            {
                temporderID = Convert.ToInt32(tempOrderDetails.TempOrderID);
                db.TempOrderDetails.Remove(tempOrderDetails);
                db.SaveChanges();
                TotalCartAmount(temporderID);
                retval = true;
            }
            return retval;

        }

        public bool SendWishlistToCart(int rowID)
        {
            bool retval = false;
            var tempOrderDetails = db.TempOrderDetails.Where(row => row.ID == rowID).FirstOrDefault();
            if (tempOrderDetails != null)
            {
                tempOrderDetails.IsUserWishList = false;
                db.SaveChanges();
                TotalCartAmount(tempOrderDetails.TempOrderID ?? 0);
                retval = true;
            }
            return retval;
        }

    }
}

