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
    public class RepoSubcategory
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        #endregion

        #region Add Or Update SubCategory
        public bool AddOrUpdateSubCategory(SubCategoryModel model)
        {
            var subCategory = db.SubCategories.Where(a => a.ID == model.SubCategoryId).FirstOrDefault();
            if (subCategory != null)
            {
                subCategory.CategoryID = model.CategoryId;
                subCategory.SubCatName = model.SubCategoryName;
                subCategory.SubCatImage = model.ImagePath;
                subCategory.LastUpdate = DateTime.Now;
                subCategory.IsActive = model.IsActive;
                subCategory.GST = model.GST;
                subCategory.SGST = model.SGST;
                subCategory.IGST = model.IGST;
            }
            else
            {
                db.SubCategories.Add(new SubCategory()
                {
                    CategoryID = model.CategoryId,
                    SubCatName = model.SubCategoryName,
                    SubCatImage = model.ImagePath,
                    Adddate = DateTime.Now,
                    IsActive = model.IsActive,
                    IsDelete = false,
                    GST = model.GST,
                    SGST = model.SGST,
                    IGST = model.IGST
            });
            }
            return db.SaveChanges() > 0;
        }

        #endregion

        #region Get All SubCategory
        public List<SubCategory> GetAllSubCategory()
        {
            return db.SubCategories.Include(a => a.Category).AsNoTracking().ToList();
        }
        #endregion

        #region Get SubCategory By Id
        public SubCategoryModel GetSubCategoryById(int subCategoryId)
        {
            var subCategory = db.SubCategories.Where(x => x.ID == subCategoryId).Select(a => new SubCategoryModel()
            {
                CategoryId = a.CategoryID.Value,
                SubCategoryId = a.ID,
                SubCategoryName = a.SubCatName,
                ImagePath = a.SubCatImage,
                IsActive = a.IsActive.Value
            }).FirstOrDefault();

            return subCategory;

        }
        #endregion

        #region Delete SubCategory By Id
        public bool DeleteSubCategoryById(int subCategoryId)
        {
            var subCategory = db.SubCategories.Where(x => x.ID == subCategoryId).FirstOrDefault();
            if (subCategory != null)
            {
                db.SubCategories.Remove(subCategory);
            }
            return db.SaveChanges() > 0;
        }
        #endregion

        #region Get SubCategory DDl
        /// <summary>
        /// Category Id is nullable field for getting bassed on that subcategory
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public SelectList GetSubCategoryDDl(int categoryId = 0)
        {
            dynamic subCaterories;
            if (categoryId == 0)
            {
                subCaterories = db.SubCategories.Where(a => a.IsActive == true && a.IsDelete == false).Select(a => new
                {
                    Text = a.SubCatName,
                    Value = a.ID
                }).AsNoTracking().ToList();
            }
            else
            {
                subCaterories = db.SubCategories.Where(a => a.IsActive == true && a.IsDelete == false && a.CategoryID == categoryId).Select(a => new
                {
                    Text = a.SubCatName,
                    Value = a.ID
                }).AsNoTracking().ToList();
            }

            return new SelectList(subCaterories, "Value", "Text");
        }
        #endregion
    }
}
