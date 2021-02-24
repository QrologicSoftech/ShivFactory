using DataLibrary.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShivFactory.Business.Repository.Product
{
    public class RepoProduct
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        #endregion

        #region Add Or Update Product
        public bool AddOrUpdateProduct(ProductModel model)
        {
            var Product = db.Products.Where(a => a.ProductId == model.ProductId).FirstOrDefault();
            if (Product != null)
            {
                Product.ProductName = model.ProductName;
                Product.MainImage = model.ImagePath;
                Product.AddUpdate = DateTime.Now;
                Product.IsActive = model.IsActive;
            }
            else
            {
                db.Products.Add(new DataLibrary.DL.Product()
                {
                    ProductName = model.ProductName,
                    MainImage = model.ImagePath,
                    AddUpdate = DateTime.Now,
                    IsActive = true,
                  
                });
            }
            return db.SaveChanges() > 0;
        }

        #endregion

        #region GetAllProduct
        public List<DataLibrary.DL.Product> GetAllProduct()
        {
            return db.Products.ToList();
        }
        #endregion

        #region Get Product By ProductId
        public ProductModel GetProductByProductId(int ProductId)
        {
            var Product = db.Products.Where(x => x.ProductId == ProductId).Select(a => new ProductModel()
            {
                ProductId = a.ProductId,
                ProductName = a.ProductName,
                ImagePath = a.MainImage,
                IsActive = a.IsActive.Value
            }).FirstOrDefault();

            return Product;

        }
        #endregion

        #region Delete Product By ProductId
        public bool DeleteProductByProductId(int ProductId)
        {
            var Product = db.Products.Where(x => x.ProductId == ProductId).FirstOrDefault();
            if (Product != null)
            {
                db.Products.Remove(Product);
            }
            return db.SaveChanges() > 0;
        }
        #endregion

        #region Get Product DDl
        public SelectList GetProductDDl()
        {
            var caterories = db.Products.Where(a => a.IsActive == true).Select(a => new
            {
                Text = a.ProductName,
                Value = a.ProductId
            }).ToList();
            return new SelectList(caterories, "Value", "Text");
        }
        #endregion
    }
}
