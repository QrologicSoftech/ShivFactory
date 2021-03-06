using DataLibrary.DL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShivFactory.Business.Repository
{
    public class RepoColor
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        #endregion

        #region Add Or Update Color
        public bool AddOrUpdateColor(ColorModel model)
        {
            var dimensionMaster = db.ColorMasters.Where(a => a.Name == model.Name).FirstOrDefault();
            if (dimensionMaster != null)
            {
                dimensionMaster.Name = model.Name;
                db.SaveChanges();
                return true;
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

        #region Get Color By Id
        public ColorModel GetColorById(int ColorMasterId)
        {
            var ColorMasters = db.ColorMasters.Where(x => x.Id == ColorMasterId).Select(a => new ColorModel()
            {
                Id = a.Id,
                Name = a.Name
            }).FirstOrDefault();

            return ColorMasters;

        }
        #endregion

        #region Get All Colors
        public List<ColorResponse> GetAllColor()
        {
            return db.ColorMasters.Select(a=> new ColorResponse() {
                Id=a.Id,
                Name=a.Name
            }).AsNoTracking().ToList();
        }
        #endregion

        #region Delete Color By Id
        public bool DeleteColorById(int ColorMasterId)
        {
            var ColorMasters = db.ColorMasters.Where(x => x.Id == ColorMasterId).FirstOrDefault();
            if (ColorMasters != null)
            {
                db.ColorMasters.Remove(ColorMasters);
            }
            return db.SaveChanges() > 0;
        }
        #endregion

        #region Get Color DDl

        public SelectList GetColorDDl()
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
