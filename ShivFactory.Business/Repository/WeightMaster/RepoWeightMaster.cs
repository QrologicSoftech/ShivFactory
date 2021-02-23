using DataLibrary.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShivFactory.Business.Repository.WeightMaster
{
    public class RepoWeightMaster
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        #endregion

        #region Add Or Update DimensionMaster
        public bool AddOrUpdateWeightMaster(WeightModel model)
        {
            var dimensionMaster = db.WeightMasters.Where(a => a.Id == model.Id).FirstOrDefault();
            if (dimensionMaster != null)
            {
                dimensionMaster.Id = model.Id;
                dimensionMaster.Name = model.Name;
            }
            else
            {
                dimensionMaster = db.WeightMasters.Add(new DataLibrary.DL.WeightMaster
                {
                    Id = model.Id,
                    Name = model.Name,
                });
            }
            return db.SaveChanges() > 0;
        }

        #endregion


        #region Get DimensionMaster By Id
        public WeightModel GetWeightMasterById(int weightMasterId)
        {
            var weightMasters = db.WeightMasters.Where(x => x.Id == weightMasterId).Select(a => new WeightModel()
            {
                Id = a.Id,
                Name = a.Name
            }).FirstOrDefault();

            return weightMasters;

        }
        #endregion

        #region Get All DimensionMaster
        public List<DataLibrary.DL.WeightMaster> GetAllWeightMaster()
        {
            return db.WeightMasters.Where(a => a.Name != null).ToList();
        }
        #endregion

        #region Delete SubCategory By Id
        public bool DeleteWeightMasterById(int weightMasterId)
        {
            var weightMasters = db.WeightMasters.Where(x => x.Id == weightMasterId).FirstOrDefault();
            if (weightMasters != null)
            {
                db.WeightMasters.Remove(weightMasters);
            }
            return db.SaveChanges() > 0;
        }
        #endregion

        #region Get DimensionMaster DDl

        public SelectList GetweightMasterDDl()
        {
            dynamic weightMaster;

            weightMaster = db.WeightMasters.Where(a => a.Name != null).Select(a => new
            {
                Text = a.Name,
                Value = a.Id
            }).ToList();
            return new SelectList(weightMaster, "Value", "Text");
        }
        #endregion
    }
}
