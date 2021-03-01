using DataLibrary.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShivFactory.Business.Repository
{
    public class RepoWeightMaster
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        #endregion

        #region Add Or Update DimensionMaster
        public bool AddOrUpdateWeight(WeightModel model)
        {
            var dimensionMaster = db.WeightMasters.Where(a => a.Name == model.Name).FirstOrDefault();
            if (dimensionMaster != null)
            {
                dimensionMaster.Name = model.Name;
                db.SaveChanges();
                return true;
            }
            else
            {
                dimensionMaster = db.WeightMasters.Add(new WeightMaster
                {
                    Id = model.Id,
                    Name = model.Name,
                });
            }
            return db.SaveChanges() > 0;
        }

        #endregion

        #region Get Dimension By Id
        public WeightModel GetWeightById(int weightId)
        {
            var weightMasters = db.WeightMasters.Where(x => x.Id == weightId).Select(a => new WeightModel()
            {
                Id = a.Id,
                Name = a.Name
            }).FirstOrDefault();

            return weightMasters;

        }
        #endregion

        #region Get All Dimension
        public List<WeightMaster> GetAllWeight()
        {
            return db.WeightMasters.Where(a => a.Name != null).ToList();
        }
        #endregion

        #region Delete SubCategory By Id
        public bool DeleteWeightById(int weightMasterId)
        {
            var weightMasters = db.WeightMasters.Where(x => x.Id == weightMasterId).FirstOrDefault();
            if (weightMasters != null)
            {
                db.WeightMasters.Remove(weightMasters);
            }
            return db.SaveChanges() > 0;
        }
        #endregion

        #region Get Dimension DDl

        public SelectList GetweightDDl()
        {
            dynamic weight;

            weight = db.WeightMasters.Where(a => a.Name != null).Select(a => new
            {
                Text = a.Name,
                Value = a.Id
            }).ToList();
            return new SelectList(weight, "Value", "Text");
        }
        #endregion
    }
}
