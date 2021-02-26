﻿using DataLibrary.DL;
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
                Product.Description = model.Description;
                Product.AddUpdate = DateTime.Now;
                Product.IsActive = model.IsActive;
                Product.SalePrice = model.SalePrice;
                Product.ListPrice = model.ListPrice;
                Product.Category = model.Category;
                Product.SubCategory = model.SubCategory;
                Product.MiniCategory = model.MiniCategory;
                Product.MainImage = model.ImagePath;
                if (model.imgPathList.Count > 0)
                {
                    Product.Image1 = model.imgPathList[0];
                    Product.Image2 = model.imgPathList[1];
                    Product.Image3 = model.imgPathList[2];
                    Product.Image4 = model.imgPathList[3];
                    Product.Image5 = model.imgPathList[4];
                }
            }
            else
            {
                string imgpth1 = "", imgpth2 = "", imgpth3 = "", imgpth4 = "", imgpth5 = ""; 
                if (!(model.imgPathList==null))
                {
                    imgpth1 = model.imgPathList[0];
                    imgpth2 = model.imgPathList[1];
                    imgpth3 = model.imgPathList[2];
                    imgpth4 = model.imgPathList[3];
                    imgpth5 = model.imgPathList[4];
                }
                db.Products.Add(new DataLibrary.DL.Product()
                {
                    ProductName = model.ProductName,
                    Description = model.Description,
                    AddUpdate = DateTime.Now,
                    IsActive = true,
                    SalePrice = model.SalePrice,
                ListPrice = model.ListPrice,
                Category = model.Category,
                SubCategory = model.SubCategory,
                MiniCategory = model.MiniCategory,
                MainImage = model.ImagePath,
                    Image1 = imgpth1,
                    Image2 = imgpth2,
                    Image3 = imgpth3,
                    Image4 = imgpth4,
                    Image5 = imgpth5


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
                IsActive = a.IsActive.Value,
                Description = a.Description,
                
                SalePrice = Convert.ToDecimal(a.SalePrice),
                ListPrice = Convert.ToDecimal(a.ListPrice),
                Category = (int)a.Category,
                SubCategory = (int)a.SubCategory,
                MiniCategory = (int)a.MiniCategory
               
               
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
