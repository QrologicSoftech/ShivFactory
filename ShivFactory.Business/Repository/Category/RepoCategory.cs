using DataLibrary.DL;
using ShivFactory.Business.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace ShivFactory.Business.Repository
{
    public class RepoCategory
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        #endregion

        #region Add Or Update Category
        public bool AddOrUpdateCategory(CategoryModel model)
        {
            var category = db.Categories.Where(a => a.ID == model.CategoryId).FirstOrDefault();
            if (category != null)
            {
                category.CategoryName = model.CategoryName;
                category.CatImage = model.ImagePath;
                category.LastUpdate = DateTime.Now;
                category.IsActive = model.IsActive;
            }
            else
            {
                db.Categories.Add(new Category()
                {
                    CategoryName = model.CategoryName,
                    CatImage = model.ImagePath,
                    Adddate = DateTime.Now,
                    IsActive = true,
                    IsDelete = false
                });
            }
            return db.SaveChanges() > 0;
        }

        #endregion

        #region GetAllCategory
        public List<Category> GetAllCategory()
        {
            return db.Categories.AsNoTracking().ToList();
        }
        #endregion

        #region Get Category By CategoryId
        public CategoryModel GetCategoryByCategoryId(int categoryId)
        {
            var category = db.Categories.Where(x => x.ID == categoryId).Select(a => new CategoryModel()
            {
                CategoryId = a.ID,
                CategoryName = a.CategoryName,
                ImagePath = a.CatImage,
                IsActive = a.IsActive
            }).FirstOrDefault();

            return category;

        }
        #endregion

        #region Delete Category By CategoryId
        public bool DeleteCategoryByCategoryId(int categoryId)
        {
            var category = db.Categories.Where(x => x.ID == categoryId).FirstOrDefault();
            if (category != null)
            {
                db.Categories.Remove(category);
            }
            return db.SaveChanges() > 0;
        }
        #endregion

        #region Get Category DDl
        public SelectList GetCategoryDDl()
        {
            var caterories = db.Categories.Where(a => a.IsActive == true && a.IsDelete == false).Select(a => new
            {
                Text = a.CategoryName,
                Value = a.ID
            }).AsNoTracking().ToList();
            return new SelectList(caterories, "Value", "Text");
        }
        #endregion
    }
}
