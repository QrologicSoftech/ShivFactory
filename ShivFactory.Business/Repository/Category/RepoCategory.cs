using DataLibrary.DL;
using ShivFactory.Business.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace ShivFactory.Business.Repository.Category
{
    public class RepoCategory
    {
        #region Add Or Update Category
        public bool AddOrUpdateCategory(CategoryModel catModel)
        {
            using (var context = new ShivFactoryEntities())
            {
                Category category = new Category()
                {
                    CategoryName = catModel.CategoryName,
                    CatImage = catModel.CatImage,
                    Adddate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    IsActive = true,
                    IsDelete = false
                };
                context.Categories.Add(category);
                context.SaveChanges();
                return category.ID;
            }
        }

        #endregion
    }
}
