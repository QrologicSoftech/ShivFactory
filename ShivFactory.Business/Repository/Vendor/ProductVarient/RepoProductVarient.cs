using DataLibrary.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
    public class RepoProductVarient
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        #endregion

        #region Update Varient Quantity
        public bool UpdateVarientQuantity(int varientId, int qty)
        {
            var product = db.ProductVarients.Where(x => x.ProductVarientId == varientId).FirstOrDefault();
            if (product != null)
            {
                product.Stock = qty;
                return db.SaveChanges() > 0;
            }
            return false;
        }
        #endregion

        #region Delete ProductVarient By Id
        public bool DeleteProductVarientById(int id)
        {
            var Varient = db.ProductVarients.Where(x => x.ProductVarientId == id).FirstOrDefault();
            if (Varient != null)
            {
                db.ProductVarients.Remove(Varient);
            }
            return db.SaveChanges() > 0;
        }
        #endregion
    }
}
