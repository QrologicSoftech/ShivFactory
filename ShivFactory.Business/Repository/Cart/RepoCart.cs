using DataLibrary.DL;
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
            db.TempOrders.Add(new TempOrder()
            {
                productID = model.ProductID,
                ProductName = model.ProductName,
                productVarientID = model.ProductVarientID,
                Price = Convert.ToDecimal(model.Price),
                Brand = model.Brand,
                Quantity = Convert.ToInt32(model.Quantity)
            });
            return db.SaveChanges() > 0;
        
        }
    }
}
