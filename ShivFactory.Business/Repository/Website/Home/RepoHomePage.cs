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
                ImagePath = a.BannerImage
            }).Take(10).AsNoTracking().ToList();

            response.Products = db.SubCategories.Where(a => a.ShowAtHome == true && a.IsActive == true && a.IsDelete == false).Include(a => a.Category).OrderByDescending(a => a.Adddate).GroupBy(a => a.CategoryID).Select(a => new HomeCategoryResponse()
            {

                Id = a.Key ?? 0,
                Title = a.Select(i => i.Category != null ? i.Category.HomeTitle != null ? i.Category.HomeTitle : i.Category.CategoryName : i.SubCategoryName).FirstOrDefault(),
                SubCategory = a.Select(b => new Images()
                {
                    ImagePath = b.ImagePath,
                    SubCategoryId = b.ID,
                    SubCategoryName = b.SubCategoryName,
                }).Take(10).ToList()

            }).AsNoTracking().ToList();

            foreach (var b in response.Banners)
            {
                b.ImagePath = b.ImagePath.ImagePath();
            }

            //foreach (var c in subcategories)
            //{
            //    var subIds = c.SubCategory.Select(a => a.SubCategoryId).ToList();
            //    var p = db.Products.Where(a => a.SalePrice != null && subIds.Contains(a.SubCategoryId ?? 0)).Select(a => new { price = a.SalePrice ?? 0, listPrice = a.ListPrice ?? 0, imagePath = a.MainImage, subCategoryId = a.SubCategoryId }).AsNoTracking().ToList();

            //    response.Products.Add(new HomeCategoryResponse()
            //    {
            //        Id = c.Id,
            //        Title = c.Title,
            //        SubCategory = c.SubCategory.Select(b => new Images()
            //        {
            //            SubCategoryId = b.SubCategoryId,
            //            SubCategoryName = b.SubCategoryName,
            //            ImagePath = p.Where(a => a.subCategoryId == b.SubCategoryId)?.Select(a => a.imagePath).FirstOrDefault(),
            //            price = p.Where(a => a.subCategoryId == b.SubCategoryId)?.Select(a => a.price).FirstOrDefault().PriceFormat(),
            //            ListPrice = p.Where(a => a.subCategoryId == b.SubCategoryId)?.Select(a => a.listPrice).FirstOrDefault().PriceFormat()
            //        }).Take(10).ToList(),
            //        SubcategoryImages = c.SubCategory.Select(b => b.ImagePath.ImagePath()).Take(10).ToList()
            //    });
            //}










            foreach (var b in response.Products)
            {
                var imageList = new List<string>();
                int i = 1;
                foreach (var s in b.SubCategory)
                {
                    var c = db.Products.Where(p => p.SalePrice != null && p.SubCategoryId == s.SubCategoryId).Select(p => new { imagepath = p.MainImage, price = p.SalePrice ?? 0, listPrice = p.ListPrice ?? 0 }).AsNoTracking().FirstOrDefault();
                    s.ImagePath = c != null && !string.IsNullOrEmpty(c.imagepath) ? c.imagepath.ImagePath() : s.ImagePath.ImagePath();
                    s.price = c != null ? c.price.PriceFormat() : "";
                    s.ListPrice = c != null ? c.listPrice.PriceFormat() : "";

                    var image = s.ImagePath != null ? s.ImagePath : c != null ? c.imagepath : string.Empty;
                    if (!string.IsNullOrEmpty(image) && imageList.Count < 3)
                    {
                        imageList.Add(image.ImagePath());
                    }
                    if (i == 10 && imageList.Count < 3)
                    {
                    AddImge:
                        if (imageList.Count < 3)
                        {
                            imageList.Add(image.ImagePath());
                            goto AddImge;
                        }

                    }

                    i++;
                }

                b.SubcategoryImages = imageList;
            }


            return response;
        }

        #endregion
    }
}
