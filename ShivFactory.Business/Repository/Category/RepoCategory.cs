using DataLibrary.DL;
using ShivFactory.Business.Model;
using ShivFactory.Business.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
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
                IsActive = a.IsActive.Value
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

        #region GetAllCategory
        public List<CategoryResponse> GetAllCategories(PaginationRequest model, out int totalRecords)
        {
            RepoCommon common = new RepoCommon();
            var categories = new List<CategoryResponse>();
            totalRecords = 0;

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Action", "GetAllCategory"));
            parameters.Add(new SqlParameter("@SearchText", model.searchText));
            parameters.Add(new SqlParameter("@Skip", model.Skip));
            parameters.Add(new SqlParameter("@Take", model.PageSize));
            parameters.Add(new SqlParameter("@OrderColumn", model.SortColumn));
            parameters.Add(new SqlParameter("@OrderDir", model.SortDirection));

            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnectionString, "ManageCategory", parameters.ToArray());
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                totalRecords = ds.Tables[0].Rows[0]["TotalRow"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["TotalRow"].ToString()) : 0;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    categories.Add(new CategoryResponse()
                    {
                        SrNo = row["SrNo"] != DBNull.Value ? Convert.ToInt32(row["SrNo"]) : 0,
                        Id = row["CategoryId"] != DBNull.Value ? Convert.ToInt32(row["CategoryId"]) : 0,
                        CategoryName = row["CategoryName"] != DBNull.Value ? row["CategoryName"].ToString() : "",
                        ImagePath = common.checkfile(row["ImagePath"].ToString()),
                        IsActive = row["IsActive"] != DBNull.Value ? Convert.ToBoolean(row["IsActive"]) : false,
                        AddDate = row["Adddate"] != DBNull.Value ? Convert.ToDateTime(row["Adddate"]).ToString("dd/MM/yyyy") : ""
                    });
                }

            }

            return categories;
        }
        #endregion
    }
}
