using DataLibrary.DL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository.Website
{
    public class RepoMenu
    {
        #region Entity
        ShivFactoryEntities db = new ShivFactoryEntities();
        #endregion

        #region Get All Menues
        public List<MenuResponse> GetAllMenues()
        {
            var menue = new List<MenuResponse>();
            var category = db.Categories.Include(a => a.SubCategories).Where(a => a.IsActive == true && a.IsDelete == false).AsNoTracking().ToList();
            foreach (var c in category)
            {
                var data = new MenuResponse()
                {
                    Id = c.ID,
                    Name = c.CategoryName,
                    SubCategory = c.SubCategories.Where(a => a.IsActive == true && a.IsDelete == false).Select(a => new MenuModel()
                    {
                        Id = a.ID,
                        Name = a.SubCategoryName,
                        MiniCategory = a.MiniCategories.Where(m => m.IsActive == true && m.IsDelete == false).Select(m => new MenuModel()
                        {
                            Id = m.ID,
                            Name = m.MiniCategoryName
                        }).ToList()
                    }).ToList()
                };
                menue.Add(data);
            }
            return menue;
        }

        #endregion
    }
}
