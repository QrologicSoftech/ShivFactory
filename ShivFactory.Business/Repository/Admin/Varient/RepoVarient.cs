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
    public class RepoVarient
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        #endregion

        #region Add Or Update Varient
        public bool AddOrUpdateVarient(ClsVarient model)
        {
            var Varient = db.Varients.Where(a => a.VarientName == model.Varient).FirstOrDefault();
            if (Varient != null)
            {
                Varient.VarientName = model.Varient;
                Varient.IsActive = model.IsActive;
            }
            else
            {
                db.Varients.Add(new Varient()
                {
                    VarientName = model.Varient,
                    IsActive = model.IsActive
                });
            }
            return db.SaveChanges() > 0;
        }

        #endregion

        #region CheckAlreadytExist
        public bool CheckAlreadytExist(string varientName)
        {
            var varient = db.Varients.Where(x => x.VarientName == varientName).AsNoTracking().FirstOrDefault();
            if (varient != null) { return true; }
            return false;
        }
        #endregion

        #region Get Varient By Id
        public ClsVarient GetVarientById(int Id)
        {
            var Varient = db.Varients.Where(x => x.Id == Id).Select(a => new ClsVarient()
            {
                Id = a.Id,
                Varient = a.VarientName,
                IsActive = a.IsActive.Value
            }).FirstOrDefault();

            return Varient;

        }
        #endregion

        #region Delete Varient By Id
        public bool DeleteVarientById(int id)
        {
            var Varient = db.Varients.Where(x => x.Id == id).FirstOrDefault();
            if (Varient != null)
            {
                db.Varients.Remove(Varient);
            }
            return db.SaveChanges() > 0;
        }
        #endregion

        #region Get Varient DDl
        /// <summary>
        /// SubCategory Id is nullable field for getting bassed on that SubCategory
        /// </summary>
        /// <param name="subCategoryId"></param>
        /// <returns></returns>
        public SelectList GetVarientDDl(int subCategoryId = 0)
        {
            dynamic Varients;
            //var category = db.SubCategories.Where(a => a.ID == subCategoryId).FirstOrDefault();
            //if (category !=null)
            //{
            //    var varientIds = category.Varients;
            //    Varients = db.Varients.Where(a => a.IsActive == true&& varientIds.Contains(a.Id)).Select(a => new
            //    {
            //        Text = a.VarientName,
            //        Value = a.Id
            //    }).AsNoTracking().ToList();
            //}
            //else
            //{
                Varients = db.Varients.Where(a => a.IsActive == true).Select(a => new
                {
                    Text = a.VarientName,
                    Value = a.Id
                }).AsNoTracking().ToList();
            //}

            return new SelectList(Varients, "Value", "Text");
        }
        #endregion

        #region GetAllVarients
        public List<VarientResponse> GetAllVarients(PaginationRequest model, out int totalRecords)
        {
            var Varients = new List<VarientResponse>();
            totalRecords = 0;

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Action", "GetAllVarient"));
            parameters.Add(new SqlParameter("@SearchText", model.searchText));
            parameters.Add(new SqlParameter("@Skip", model.Skip));
            parameters.Add(new SqlParameter("@Take", model.PageSize));
            parameters.Add(new SqlParameter("@OrderColumn", model.SortColumn));
            parameters.Add(new SqlParameter("@OrderDir", model.SortDirection));

            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnectionString, "ManageVarient", parameters.ToArray());
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                totalRecords = ds.Tables[0].Rows[0]["TotalRow"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["TotalRow"].ToString()) : 0;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Varients.Add(new VarientResponse()
                    {
                        SrNo = row["SrNo"] != DBNull.Value ? Convert.ToInt32(row["SrNo"]) : 0,
                        Id = row["Id"] != DBNull.Value ? Convert.ToInt32(row["Id"]) : 0,
                        VarientName = row["VarientName"] != DBNull.Value ? row["VarientName"].ToString() : "",
                        IsActive = row["IsActive"] != DBNull.Value ? Convert.ToBoolean(row["IsActive"]) : false
                    });
                }
            }

            return Varients;
        }
        #endregion
    }
}
