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
    public class RepoBrand
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        #endregion

        #region Add Or Update Brand
        public bool AddOrUpdateBrand(ClsBrand model)
        {
            var brand = db.Brands.Where(a => a.ID == model.Id).FirstOrDefault();
            if (brand != null)
            {
                brand.CategoryId = model.CategoryId;
                brand.BrandName = model.BrandName;
                brand.ImagePath = model.ImagePath;
                brand.LastUpdate = DateTime.Now;
                brand.IsActive = model.IsActive;
            }
            else
            {
                db.Brands.Add(new Brand()
                {
                    CategoryId = model.CategoryId,
                    BrandName = model.BrandName,
                    ImagePath = model.ImagePath,
                    Adddate = DateTime.Now,
                    IsActive = model.IsActive,
                    IsDelete = false
                });
            }
            return db.SaveChanges() > 0;
        }

        #endregion

        #region Get Brand By Id
        public ClsBrand GetBrandById(int Id)
        {
            var brand = db.Brands.Where(x => x.ID == Id).Select(a => new ClsBrand()
            {
                Id=a.ID,
                CategoryId = a.CategoryId.Value,
                BrandName = a.BrandName,
                ImagePath = a.ImagePath,
                IsActive = a.IsActive.Value
            }).FirstOrDefault();

            return brand;

        }
        #endregion

        #region Delete Brand By Id
        public bool DeleteBrandById(int id)
        {
            var brand = db.Brands.Where(x => x.ID == id).FirstOrDefault();
            if (brand != null)
            {
                db.Brands.Remove(brand);
            }
            return db.SaveChanges() > 0;
        }
        #endregion

        #region Get Brand DDl
        /// <summary>
        /// Category Id is nullable field for getting bassed on that category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public SelectList GetBrandDDl(int categoryId = 0)
        {
            dynamic brands;
            if (categoryId == 0)
            {
                brands = db.Brands.Where(a => a.IsActive == true && a.IsDelete == false).Select(a => new
                {
                    Text = a.BrandName,
                    Value = a.ID
                }).AsNoTracking().ToList();
            }
            else
            {
                brands = db.Brands.Where(a => a.IsActive == true && a.IsDelete == false && a.CategoryId == categoryId).Select(a => new
                {
                    Text = a.BrandName,
                    Value = a.ID
                }).AsNoTracking().ToList();
            }

            return new SelectList(brands, "Value", "Text");
        }
        #endregion

        #region GetAllBrands
        public List<BrandResponse> GetAllBrands(PaginationRequest model, out int totalRecords)
        {
            var brands = new List<BrandResponse>();
            totalRecords = 0;

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Action", "GetAllBrand"));
            parameters.Add(new SqlParameter("@SearchText", model.searchText));
            parameters.Add(new SqlParameter("@Skip", model.Skip));
            parameters.Add(new SqlParameter("@Take", model.PageSize));
            parameters.Add(new SqlParameter("@OrderColumn", model.SortColumn));
            parameters.Add(new SqlParameter("@OrderDir", model.SortDirection));

            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnectionString, "ManageBrand", parameters.ToArray());
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                totalRecords = ds.Tables[0].Rows[0]["TotalRow"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["TotalRow"].ToString()) : 0;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    brands.Add(new BrandResponse()
                    {
                        SrNo = row["SrNo"] != DBNull.Value ? Convert.ToInt32(row["SrNo"]) : 0,
                        Id = row["Id"] != DBNull.Value ? Convert.ToInt32(row["Id"]) : 0,
                        ImagePath = row["ImagePath"] != DBNull.Value ? row["ImagePath"].ToString() : "",
                        BrandName = row["BrandName"] != DBNull.Value ? row["BrandName"].ToString() : "",
                        IsActive = row["IsActive"] != DBNull.Value ? Convert.ToBoolean(row["IsActive"]) : false,
                        AddDate = row["Adddate"] != DBNull.Value ? Convert.ToDateTime(row["Adddate"]).ToString("dd/MM/yyyy") : "",
                        CategoryName = row["CategoryName"] != DBNull.Value ? row["CategoryName"].ToString() : ""
                    });
                }

            }

            return brands;
        }
        #endregion
    }
}
