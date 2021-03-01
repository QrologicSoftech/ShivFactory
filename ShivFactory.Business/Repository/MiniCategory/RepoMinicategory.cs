using DataLibrary.DL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShivFactory.Business.Repository
{
    public class RepoMinicategory
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        #endregion

        #region Add Or Update MiniCategory
        public bool AddOrUpdateMiniCategory(MiniCategoryModel model)
        {
            var miniCategory = db.MiniCategories.Where(a => a.ID == model.MiniCategoryId).FirstOrDefault();
            if (miniCategory != null)
            {
                miniCategory.SubCategoryId = model.SubCategoryId;
                miniCategory.MiniCategoryName = model.MiniCategoryName;
                miniCategory.ImagePath = model.ImagePath;
                miniCategory.LastUpdate = DateTime.Now;
                miniCategory.IsActive = model.IsActive;
            }
            else
            {
                db.MiniCategories.Add(new MiniCategory()
                {
                    SubCategoryId = model.SubCategoryId,
                    MiniCategoryName = model.MiniCategoryName,
                    ImagePath = model.ImagePath,
                    Adddate = DateTime.Now,
                    IsActive = model.IsActive,
                    IsDelete = false
                });
            }
            return db.SaveChanges() > 0;
        }

        #endregion

        #region Get All MiniCategory
        public List<MiniCategory> GetAllMiniCategory()
        {
            return db.MiniCategories.Include(a=>a.SubCategory).AsNoTracking().ToList();
        }
        #endregion

        #region Get MiniCategory By Id
        public MiniCategoryModel GetMiniCategoryById(int miniCategoryId)
        {
            var subCategory = db.MiniCategories.Where(x => x.ID == miniCategoryId).Select(a => new MiniCategoryModel()
            {
                MiniCategoryId = a.ID,
                SubCategoryId = a.SubCategoryId.Value,
                MiniCategoryName = a.MiniCategoryName,
                ImagePath = a.ImagePath,
                IsActive = a.IsActive.Value
            }).FirstOrDefault();

            return subCategory;

        }
        #endregion

        #region Delete SubCategory By Id
        public bool DeleteMiniCategoryById(int miniCategoryId)
        {
            var miniCategory = db.MiniCategories.Where(x => x.ID == miniCategoryId).FirstOrDefault();
            if (miniCategory != null)
            {
                db.MiniCategories.Remove(miniCategory);
            }
            return db.SaveChanges() > 0;
        }
        #endregion

        #region Get MiniCategory DDl
        /// <summary>
        /// Category Id is nullable field for getting bassed on that subcategory
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public SelectList GetMiniCategoryDDl(int subcategoryId = 0)
        {
            dynamic miniCaterories;
            if (subcategoryId == 0)
            {
                miniCaterories = db.MiniCategories.Where(a => a.IsActive == true && a.IsDelete == false).Select(a => new
                {
                    Text = a.MiniCategoryName,
                    Value = a.ID
                }).AsNoTracking().ToList();
            }
            else
            {
                miniCaterories = db.MiniCategories.Where(a => a.IsActive == true && a.IsDelete == false && a.SubCategoryId == subcategoryId).Select(a => new
                {
                    Text = a.MiniCategoryName,
                    Value = a.ID
                }).AsNoTracking().ToList();
            }

            return new SelectList(miniCaterories, "Value", "Text");
        }
        #endregion
    }
}
