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

        #region GetAllMiniCategory
        public List<MiniCategoryResponse> GetAllMiniCategories(PaginationRequest model, out int totalRecords)
        {
            var categories = new List<MiniCategoryResponse>();
            totalRecords = 0;
            RepoCommon common = new RepoCommon();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Action", "GetAllMiniCategory"));
            parameters.Add(new SqlParameter("@SearchText", model.searchText));
            parameters.Add(new SqlParameter("@Skip", model.Skip));
            parameters.Add(new SqlParameter("@Take", model.PageSize));
            parameters.Add(new SqlParameter("@OrderColumn", model.SortColumn));
            parameters.Add(new SqlParameter("@OrderDir", model.SortDirection));

            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnectionString, "ManageMiniCategory", parameters.ToArray());
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                totalRecords = ds.Tables[0].Rows[0]["TotalRow"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["TotalRow"].ToString()) : 0;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    categories.Add(new MiniCategoryResponse()
                    {
                        SrNo = row["SrNo"] != DBNull.Value ? Convert.ToInt32(row["SrNo"]) : 0,
                        Id = row["Id"] != DBNull.Value ? Convert.ToInt32(row["Id"]) : 0,
                        ImagePath = common.checkfile(row["ImagePath"].ToString()),
                        MiniCategoryName = row["MiniCategoryName"] != DBNull.Value ? row["MiniCategoryName"].ToString() : "",
                        IsActive = row["IsActive"] != DBNull.Value ? Convert.ToBoolean(row["IsActive"]) : false,
                        AddDate = row["Adddate"] != DBNull.Value ? Convert.ToDateTime(row["Adddate"]).ToString("dd/MM/yyyy") : "",
                        SubCategoryName = row["SubCategoryName"] != DBNull.Value ? row["SubCategoryName"].ToString() : "",
                    });
                }

            }

            return categories;
        }
        #endregion
    }
}
