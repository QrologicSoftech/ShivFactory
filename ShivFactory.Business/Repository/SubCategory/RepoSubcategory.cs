using DataLibrary.DL;
using ShivFactory.Business.Model;
using ShivFactory.Business.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
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
                subCategory.SubCategoryName = model.SubCategoryName;
                subCategory.ImagePath = model.ImagePath;
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
                    SubCategoryName = model.SubCategoryName,
                    ImagePath = model.ImagePath,
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
                SubCategoryName = a.SubCategoryName,
                ImagePath = a.ImagePath,
                IsActive = a.IsActive.Value
            }).FirstOrDefault();

            return subCategory;

        }


        public List<SubCategory> GetSubCategoryByCatId(int subCategoryId)
        {
            var query = from SubCategory in db.SubCategories
                        where SubCategory.CategoryID == subCategoryId
                        select SubCategory;
            var subCategory = query.ToList<SubCategory>();
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
                    Text = a.SubCategoryName,
                    Value = a.ID
                }).AsNoTracking().ToList();
            }
            else
            {
                subCaterories = db.SubCategories.Where(a => a.IsActive == true && a.IsDelete == false && a.CategoryID == categoryId).Select(a => new
                {
                    Text = a.SubCategoryName,
                    Value = a.ID
                }).AsNoTracking().ToList();
            }

            return new SelectList(subCaterories, "Value", "Text");
        }
        #endregion

        #region GetAllSubCategory
        public List<SubCategoryResponse> GetAllSubCategories(PaginationRequest model, out int totalRecords)
        {
            var categories = new List<SubCategoryResponse>();
            totalRecords = 0;

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Action", "GetAllSubCategory"));
            parameters.Add(new SqlParameter("@SearchText", model.searchText));
            parameters.Add(new SqlParameter("@Skip", model.Skip));
            parameters.Add(new SqlParameter("@Take", model.PageSize));
            parameters.Add(new SqlParameter("@OrderColumn", model.SortColumn));
            parameters.Add(new SqlParameter("@OrderDir", model.SortDirection));

            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnectionString, "ManageSubCategory", parameters.ToArray());
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                totalRecords = ds.Tables[0].Rows[0]["TotalRow"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["TotalRow"].ToString()) : 0;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    categories.Add(new SubCategoryResponse()
                    {
                        SrNo = row["SrNo"] != DBNull.Value ? Convert.ToInt32(row["SrNo"]) : 0,
                        Id = row["Id"] != DBNull.Value ? Convert.ToInt32(row["Id"]) : 0,
                        ImagePath = row["ImagePath"] != DBNull.Value ? row["ImagePath"].ToString() : "",
                        SubCategoryName = row["SubCategoryName"] != DBNull.Value ? row["SubCategoryName"].ToString() : "",
                        GST = row["GST"] != DBNull.Value ? row["GST"].ToString() : "",
                        SGST = row["SGST"] != DBNull.Value ? row["SGST"].ToString() : "",
                        IGST = row["IGST"] != DBNull.Value ? row["IGST"].ToString() : "",
                        IsActive = row["IsActive"] != DBNull.Value ? Convert.ToBoolean(row["IsActive"]) : false,
                        AddDate = row["Adddate"] != DBNull.Value ? Convert.ToDateTime(row["Adddate"]).ToString("dd/MM/yyyy") : "",
                        CategoryName = row["CategoryName"] != DBNull.Value ? row["CategoryName"].ToString() : ""
                    });
                }

            }

            return categories;
        }
        #endregion
    }
}
