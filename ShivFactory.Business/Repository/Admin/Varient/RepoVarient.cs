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
            var Varient = db.Varients.Where(a => a.Id == model.Id).FirstOrDefault();
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
        /// /// <summary>
        /// varientNames is nullable varientNames string 
        /// </summary>
        /// <param name="subCategoryId"></param>
        /// <param name="varientNames"></param>
        /// <returns></returns>
        /// int subCategoryId = 0, string varientNames=null
        public SelectList GetVarientDDl(int subCategoryId, string varientNames)
        {
            var sqlQuery = db.Varients.Where(a => a.IsActive == true);
            var category = db.SubCategories.Where(a => a.ID == subCategoryId).FirstOrDefault();
            if (category != null && !string.IsNullOrEmpty(category.Varients))
            {
                List<int> varientIds = category.Varients.Split(',').Select(int.Parse).ToList();
                sqlQuery = sqlQuery.Where(a => varientIds.Contains(a.Id));
            }
            if (!string.IsNullOrEmpty(varientNames))
            {
                List<string> varientList = varientNames.Trim().Split(',').ToList();
                sqlQuery = sqlQuery.Where(a => !varientList.Contains(a.VarientName));
            }

            var Varients = sqlQuery.Select(a => new
            {
                Text = a.VarientName,
                Value = a.Id
            }).AsNoTracking().ToList();


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

        #region GetAllVarients without pagination
        public List<VarientResponse> GetAllVarients()
        {
            string searchText;
            List<VarientResponse> varients = new List<VarientResponse>();
            varients = db.Varients.Where(a => a.IsActive == true).Select(a => new VarientResponse()
            {
                Id = a.Id,
                VarientName = a.VarientName
            }).AsNoTracking().ToList();
            //int size = 50;
            //var varientQuery = db.Varients.Where(a => a.IsActive == true);

            //if (!string.IsNullOrEmpty(searchText))
            //{
            //    varientQuery = varientQuery.Where(a => a.VarientName.StartsWith(searchText));
            //}
            //varients = varientQuery.Take(size).Select(a => new VarientResponse()
            //{
            //    Id = a.Id,
            //    VarientName = a.VarientName
            //}).AsNoTracking().ToList();

            return varients;

        }
        #endregion

        #region GetVarientsFilter
        public List<VarientsFilter> GetVarientsFilter(ProductListingPagination model)
        {
            var Varients = new List<VarientsFilter>();

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Action", "GetProductVarients"));
            parameters.Add(new SqlParameter("@SearchText", ""));
            parameters.Add(new SqlParameter("@CategoryId", ""));
            parameters.Add(new SqlParameter("@SubCategoryId", 5));
            parameters.Add(new SqlParameter("@MiniCategoryId", ""));

            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnectionString, CommandType.StoredProcedure, "ManageVarient", parameters.ToArray());
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                Varients = ds.Tables[0].AsEnumerable().GroupBy(item => item.Field<string>("VarientName"))
          .Select(group => new VarientsFilter()
          {
              VarientName = group.Key,
              VarientValue = group.Select(item => item.Field<string>("VarientValue")).ToList()
          }).ToList();

            }

            return Varients;
        }
        #endregion
    }
}
