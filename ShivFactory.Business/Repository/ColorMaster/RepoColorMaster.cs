using DataLibrary.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShivFactory.Business.Repository.ColorMaster
{
    public class RepoColorMaster
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        #endregion

        #region Add Or Update ColorMaster
        public bool AddOrUpdateColorMaster(ColorModel model)
        {
            var dimensionMaster = db.ColorMasters.Where(a => a.Id == model.Id).FirstOrDefault();
            if (dimensionMaster != null)
            {
                dimensionMaster.Id = model.Id;
                dimensionMaster.Name = model.Name;
            }
            else
            {
                dimensionMaster = db.ColorMasters.Add(new DataLibrary.DL.ColorMaster
                {
                    Id = model.Id,
                    Name = model.Name,
                });
            }
            return db.SaveChanges() > 0;
        }

        #endregion


        #region Get DimensionMaster By Id
        public ColorModel GetColorMasterById(int ColorMasterId)
        {
            var ColorMasters = db.ColorMasters.Where(x => x.Id == ColorMasterId).Select(a => new ColorModel()
            {
                Id = a.Id,
                Name = a.Name
            }).FirstOrDefault();

            return ColorMasters;

        }
        #endregion

        #region Get All DimensionMaster
        public List<DataLibrary.DL.ColorMaster> GetAllColorMaster()
        {
            return db.ColorMasters.Where(a => a.Name != null).ToList();
        }
        #endregion

        #region Delete SubCategory By Id
        public bool DeleteColorMasterById(int ColorMasterId)
        {
            var ColorMasters = db.ColorMasters.Where(x => x.Id == ColorMasterId).FirstOrDefault();
            if (ColorMasters != null)
            {
                db.ColorMasters.Remove(ColorMasters);
            }
            return db.SaveChanges() > 0;
        }
        #endregion

        #region Get DimensionMaster DDl

        public SelectList GetColorMasterDDl()
        {
            dynamic ColorMaster;

            ColorMaster = db.ColorMasters.Where(a => a.Name != null).Select(a => new
            {
                Text = a.Name,
                Value = a.Id
            }).ToList();
            return new SelectList(ColorMaster, "Value", "Text");
        }
        #endregion
    }
}
