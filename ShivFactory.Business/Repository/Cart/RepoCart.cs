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
        Utility utility = new Utility();
        #endregion
        public bool AddToCart(AddToCart model)
        {
            string userid = utility.GetCurrentUserId();
            try
            {
                int tempOrderId = Convert.ToInt32(cooki.GetCookiesValue(CookieName.TempOrderId));
                if (tempOrderId == 0)
                {
                    var tempOrder = db.TempOrders.Add(new TempOrder()
                    {
                        UserId = utility.GetCurrentUserId(),
                        AddDate = DateTime.Now
                    });
                    tempOrderId = tempOrder.ID;
                }

                var orderDetails = db.TempOrderDetails.Where(a => a.ProductVarientId == model.ProductVarientID).FirstOrDefault();
                if (orderDetails != null)
                {
                    orderDetails.Quantity = model.Quantity; //orderDetails.Quantity ?? 0 + m
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
                        NetAmt = model.Quantity * model.Price
                    };
                    db.TempOrderDetails.Add(temporderDetails);
                }
                
                 db.SaveChanges() ;
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
                int tempOrderId = Convert.ToInt32(cooki.GetCookiesValue(CookieName.TempOrderId));
                if (tempOrderId == 0)
                {
                    var tempOrder = db.TempOrders.Add(new TempOrder()
                    {
                        UserId = utility.GetCurrentUserId(),
                        AddDate = DateTime.Now
                    });
                    tempOrderId = tempOrder.ID;
                }

                var orderDetails = db.TempOrderDetails.Where(a => a.ID == model.TempOrderDetailId).FirstOrDefault();
                if (orderDetails != null)
                {
                    orderDetails.Quantity = model.Quantity; //orderDetails.Quantity ?? 0 + m
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

        public CartModel GetCart()
        {
            var model= new CartModel();
            int tempId = Convert.ToInt32(cooki.GetCookiesValue(CookieName.TempOrderId));
            var data = db.TempOrders.Where(a => a.ID == tempId).Select(a=> new CartModel
            { 
                CartValue=a.NetAmt??0
        }).FirstOrDefault();

            var items = (from tod in db.TempOrderDetails
                     join p in db.Products
                     on tod.ProductId equals p.ProductId
                     where tod.TempOrderID == tempId
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
            return data;
        }

        public void TotalCartAmount(int tempOrderId)
        {
            var NetAmt = db.TempOrderDetails.Where(row => row.TempOrderID == tempOrderId).Select(row => row.NetAmt??0).Sum();
            var tempOrder = db.TempOrders.Where(row => row.ID == tempOrderId).FirstOrDefault();
            if (tempOrder != null)
            {
                tempOrder.NetAmt = NetAmt;
            }
             db.SaveChanges() ;
        }

        public bool DeleteCartItemById(int rowID)
        {
            bool retval = false; 
            var tempOrderDetails = db.TempOrderDetails.Where(row => row.ID == rowID).FirstOrDefault();
            if (tempOrderDetails != null)
            {
             db.TempOrderDetails.Remove(tempOrderDetails) ;
                db.SaveChanges();
                retval = true;
            }
            return retval; 
           
        }

    }
}

