using DataLibrary.DL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository.Website
{
    public class RepoHomePage
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        RepoCommon common = new RepoCommon();
        #endregion

        #region Add Or Update SubCategory
        public HomePageResponse GetHomePageData()
        {
            HomePageResponse response = new HomePageResponse();

            response.Banners = db.Banners.Where(a => a.IsActive == true && a.IsDelete == false && a.BannerImage != null).OrderByDescending(a => a.Adddate).Select(a => new Images()
            {
                ImagePath = common.checkfile(a.BannerImage)
            }).Take(10).AsNoTracking().ToList();

            

            var subCategory = db.SubCategories.Where(a => a.ShowAtHome == true && a.IsActive == true && a.IsDelete == false).Include(a => a.Category).OrderByDescending(a => a.Adddate).GroupBy(a => a.CategoryID).Select(a => new HomeCategoryResponse()
            {
                Id = a.Key ?? 0,
                Title = a.Select(i => i.Category != null ? i.Category.HomeTitle != null ? i.Category.HomeTitle : i.Category.CategoryName : i.SubCategoryName).FirstOrDefault(),
                SubCategory = a.Select(b => new Images()
                {
                    ImagePath = b.ImagePath
                }).Take(10).ToList()
            }).AsNoTracking().ToList();

            response.Products = subCategory;

            return response;
        }

        #endregion
    }
}
