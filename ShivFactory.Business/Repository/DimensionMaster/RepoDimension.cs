using DataLibrary.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.DL;
using System.Web.Mvc;

namespace ShivFactory.Business.Repository.DimensionMaster
{
  public  class RepoDimension
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        #endregion

        #region Add Or Update DimensionMaster
        public bool AddOrUpdateDimensionMaster(DimensionModel model)
        {
            var dimensionMaster = db.DimensionMasters.Where(a => a.Id == model.Id).FirstOrDefault();
            if (dimensionMaster != null)
            {
                dimensionMaster.Id = model.Id;
                dimensionMaster.Name = model.Name;
            }
            else
            {
              dimensionMaster= db.DimensionMasters.Add(new DataLibrary.DL.DimensionMaster
                {
                    Id = model.Id,
                    Name = model.Name,
                });
            }
            return db.SaveChanges() > 0;
        }

        #endregion


        #region Get DimensionMaster By Id
        public DimensionModel GetDimensionMasterById(int diminsionMasterId)
        {
            var dimensionMaster = db.DimensionMasters.Where(x => x.Id == diminsionMasterId).Select(a => new DimensionModel()
            {
                Id = a.Id,
                Name = a.Name
            }).FirstOrDefault();

            return dimensionMaster;

        }
        #endregion

        #region Get All DimensionMaster
        public List<DataLibrary.DL.DimensionMaster> GetAllDimensionMaster()
        {
            return db.DimensionMasters.Where(a => a.Name != null).ToList();
        }
        #endregion

        #region Delete SubCategory By Id
        public bool DeleteDimensionMasterById(int dimensionId)
        {
            var dimensionMasters = db.DimensionMasters.Where(x => x.Id == dimensionId).FirstOrDefault();
            if (dimensionMasters != null)
            {
                db.DimensionMasters.Remove(dimensionMasters);
            }
            return db.SaveChanges() > 0;
        }
        #endregion

        #region Get DimensionMaster DDl
       
        public SelectList GetDimensionMasterDDl()
        {
            dynamic dimensionMaster;

            dimensionMaster = db.DimensionMasters.Where(a => a.Name !=null).Select(a => new
                {
                    Text = a.Name,
                    Value = a.Id
                }).ToList();
            return new SelectList(dimensionMaster, "Value", "Text");
        }
        #endregion
    }
}
