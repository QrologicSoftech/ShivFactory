using DataLibrary.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.DL;
using System.Web.Mvc;
using ShivFactory.Business.Model;
using System.Data.SqlClient;
using System.Data;
using ShivFactory.Business.Models;

namespace ShivFactory.Business.Repository
{
    public class RepoDimension
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        #endregion

        #region Add Or Update Dimension
        public bool AddOrUpdateDimension(DimensionModel model)
        {
            bool result = false;
            var dimensionMaster = db.DimensionMasters.Where(a => a.Name == model.Name).FirstOrDefault();
            if (dimensionMaster != null)
            {
                dimensionMaster.Name = model.Name;
                db.SaveChanges();
                result = true;
            }
            else
            {
                dimensionMaster = db.DimensionMasters.Add(new DimensionMaster
                {
                    Id = model.Id,
                    Name = model.Name,
                });
                return db.SaveChanges() > 0;
            }
            return result;
        }

        #endregion

        #region Get Dimension By Id
        public DimensionModel GetDimensionById(int diminsionMasterId)
        {
            var dimensionMaster = db.DimensionMasters.Where(x => x.Id == diminsionMasterId).Select(a => new DimensionModel()
            {
                Id = a.Id,
                Name = a.Name
            }).FirstOrDefault();

            return dimensionMaster;

        }
        #endregion

        #region Get All Dimension
        public List<DimensionMaster> GetAllDimension()
        {
            return db.DimensionMasters.Where(a => a.Name != null).ToList();
        }
        #endregion

        #region Delete Dimension By Id
        public bool DeleteDimensionById(int dimensionId)
        {
            var dimensionMasters = db.DimensionMasters.Where(x => x.Id == dimensionId).FirstOrDefault();
            if (dimensionMasters != null)
            {
                db.DimensionMasters.Remove(dimensionMasters);
            }
            return db.SaveChanges() > 0;
        }
        #endregion

        #region Get Dimension DDl

        public SelectList GetDimensionDDl()
        {
            dynamic dimensionMaster;

            dimensionMaster = db.DimensionMasters.Where(a => a.Name != null).Select(a => new
            {
                Text = a.Name,
                Value = a.Id
            }).ToList();
            return new SelectList(dimensionMaster, "Value", "Text");
        }
        #endregion

        #region GetAllDimensions
        public List<DimensionResponse> GetAllDimensions(PaginationRequest model, out int totalRecords)
        {
            var dimensions = new List<DimensionResponse>();
            totalRecords = 0;

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Action", "GetAllDimension"));
            parameters.Add(new SqlParameter("@SearchText", model.searchText));
            parameters.Add(new SqlParameter("@Skip", model.Skip));
            parameters.Add(new SqlParameter("@Take", model.PageSize));
            parameters.Add(new SqlParameter("@OrderColumn", model.SortColumn));
            parameters.Add(new SqlParameter("@OrderDir", model.SortDirection));

            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnectionString, "ManageDimension", parameters.ToArray());
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                totalRecords = ds.Tables[0].Rows[0]["TotalRow"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["TotalRow"].ToString()) : 0;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dimensions.Add(new DimensionResponse()
                    {
                        SrNo = row["SrNo"] != DBNull.Value ? Convert.ToInt32(row["SrNo"]) : 0,
                        Id = row["Id"] != DBNull.Value ? Convert.ToInt32(row["Id"]) : 0,
                        Name = row["Name"] != DBNull.Value ? row["Name"].ToString() : ""
                    });
                }

            }

            return dimensions;
        }
        #endregion
    }
}
