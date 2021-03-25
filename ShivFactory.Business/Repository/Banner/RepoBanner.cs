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
    public class RepoBanner
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        #endregion

        #region Add Or Update Banner
        public bool AddOrUpdateBanner(ClsBanner model)
        {
            var banner = db.Banners.Where(a => a.ID == model.Id).FirstOrDefault();
            if (banner != null)
            {
                banner.Name = model.BannerName;
                banner.Title = model.Title;
                banner.BannerImage = model.ImagePath;
                banner.LastUpdate = DateTime.Now;
                banner.IsActive = model.IsActive;
            }
            else
            {
                db.Banners.Add(new Banner()
                {
                    Name = model.BannerName,
                    Title = model.Title,
                    BannerImage = model.ImagePath,
                    Adddate = DateTime.Now,
                    IsActive = model.IsActive,
                    IsDelete = false
                });
            }
            return db.SaveChanges() > 0;
        }

        #endregion

        #region Get Banner By Id
        public ClsBanner GetBannerById(int Id)
        {
            var banner = db.Banners.Where(x => x.ID == Id).Select(a => new ClsBanner()
            {
                Id = a.ID,
                BannerName = a.Name,
                Title = a.Title,
                ImagePath = a.BannerImage,
                IsActive = a.IsActive??false
            }).FirstOrDefault();

            return banner;

        }
        #endregion

        #region Delete Banner By Id
        public bool DeleteBannerById(int id)
        {
            var banner = db.Banners.Where(x => x.ID == id).FirstOrDefault();
            if (banner != null)
            {
                db.Banners.Remove(banner);
            }
            return db.SaveChanges() > 0;
        }
        #endregion

        #region GetAllBanners
        public List<BannerResponse> GetAllBanners(PaginationRequest model, out int totalRecords)
        {
            var banners = new List<BannerResponse>();
            totalRecords = 0;

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Action", "GetAllBanner"));
            parameters.Add(new SqlParameter("@SearchText", model.searchText));
            parameters.Add(new SqlParameter("@Skip", model.Skip));
            parameters.Add(new SqlParameter("@Take", model.PageSize));
            parameters.Add(new SqlParameter("@OrderColumn", model.SortColumn));
            parameters.Add(new SqlParameter("@OrderDir", model.SortDirection));

            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnectionString, "ManageBanner", parameters.ToArray());
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                totalRecords = ds.Tables[0].Rows[0]["TotalRow"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["TotalRow"].ToString()) : 0;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    banners.Add(new BannerResponse()
                    {
                        SrNo = row["SrNo"] != DBNull.Value ? Convert.ToInt32(row["SrNo"]) : 0,
                        Id = row["Id"] != DBNull.Value ? Convert.ToInt32(row["Id"]) : 0,
                        ImagePath = row["ImagePath"] != DBNull.Value ? row["ImagePath"].ToString() : "",
                        Name = row["Name"] != DBNull.Value ? row["Name"].ToString() : "",
                        Title = row["Title"] != DBNull.Value ? row["Title"].ToString() : "",
                        IsActive = row["IsActive"] != DBNull.Value ? Convert.ToBoolean(row["IsActive"]) : false,
                        AddDate = row["Adddate"] != DBNull.Value ? Convert.ToDateTime(row["Adddate"]).ToString("dd/MM/yyyy") : ""
                    });
                }

            }

            return banners;
        }
        #endregion
    }
}
