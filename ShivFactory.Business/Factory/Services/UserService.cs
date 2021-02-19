using ShivFactory.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.DL;
using System.Web;
using System.IO;
using System.Web.Configuration;

namespace ShivFactory.Business.Factory.Services
{

    public class UserService : IUserService
    {
       
        #region Add Or Update Category
        public bool AddOrUpdateCategory(CategoryModel catModel)
        {
            using (var context = new ShivFactoryEntities())
            {
                Category category = new Category()
                {
                    CategoryName = catModel.CategoryName,
                    CatImage = catModel.CatImage,
                    Adddate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    IsActive = true,
                    IsDelete = false
                };
                context.Categories.Add(category);
                context.SaveChanges();
                return category.ID;
            }
        }

        #endregion

        #region Category
        public int SaveCategory(CategoryModel catModel)
        {
            using (var context = new ShivFactoryEntities())
            {
                Category category = new Category()
                {
                    CategoryName = catModel.CategoryName,
                    CatImage = catModel.CatImage,
                    Adddate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    IsActive = true,
                    IsDelete = false
                };
                context.Categories.Add(category);
                context.SaveChanges();
                return category.ID;
            }
        }

        public List<CategoryModel> GetAllCategory()
        {
            ShivFactoryEntities db = new ShivFactoryEntities();

            var caet = db.Categories.ToList();

            using (var context = new ShivFactoryEntities())
            {
                var result = context.Categories.Where(x => x.IsDelete == false)
                    .Select(cat => new CategoryModel()
                    {
                        ID = cat.ID,
                        CategoryName = cat.CategoryName,
                        CatImage = cat.CatImage,
                        Adddate = cat.Adddate
                    }).ToList();
                return result;
            }
        }

        public CategoryModel GetCategoryById(int id)
        {
            using (var context = new ShivFactoryEntities())
            {
                var result = context.Categories.Where(x => x.ID == id).Select(cat => new CategoryModel()
                {
                    ID = cat.ID,
                    CategoryName = cat.CategoryName,
                    CatImage = cat.CatImage,
                    Adddate = cat.Adddate
                }).FirstOrDefault();
                return result;
            }


        }
        public bool UpdateCategory(int id, CategoryModel model)
        {
            using (var context = new ShivFactoryEntities())
            {
                var category = context.Categories.FirstOrDefault(x => x.ID == id);
                if (category != null)
                {
                    category.CategoryName = model.CategoryName;
                    category.CatImage = model.CatImage != null ? model.CatImage : category.CatImage;
                    category.LastUpdate = DateTime.Now;

                }
                context.SaveChanges();
                return true;
            }
        }
        public bool DeleteCategory(int id)
        {
            using (var context = new ShivFactoryEntities())
            {
                var category = context.Categories.FirstOrDefault(x => x.ID == id);
                if (category != null)
                {
                    context.Categories.Remove(category);
                    return context.SaveChanges() > 0;
                }
                return false;
            }
        }

        #endregion

        #region SubCategory
        public int SaveSubCategory(SubCategoryModel SubcatModel)
        {
            using (var context = new ShivFactoryEntities())
            {
                SubCategory subCategory = new SubCategory()
                {
                    SubCatName = SubcatModel.SubCatName,
                    CategoryID = SubcatModel.CategoryID,
                    SubCatImage = SubcatModel.SubCatImage,
                    Adddate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    IsActive = true,
                    IsDelete = false
                };
                context.SubCategories.Add(subCategory);
                context.SaveChanges();
                return subCategory.ID;
            }
        }

        public SubCategoryModel GetSubCategoryById(int id)
        {
            using (var context = new ShivFactoryEntities())
            {
                var result = context.SubCategories.Where(x => x.ID == id)
                    .Select(subcat => new SubCategoryModel()
                    {
                        ID = subcat.ID,
                        SubCatName = subcat.SubCatName,
                        SubCatImage = subcat.SubCatImage,
                    }).FirstOrDefault();
                return result;
            }
        }

        public List<SubCategoryModel> GetAllSubCategory()
        {
            using (var context = new ShivFactoryEntities())
            {
                var result = context.SubCategories.Where(x => x.IsDelete == false)
                    .Select(subcat => new SubCategoryModel()
                    {
                        ID = subcat.ID,
                        SubCatName = subcat.SubCatName,
                        SubCatImage = subcat.SubCatImage,
                        Adddate = subcat.Adddate
                    }).ToList();
                return result;
            }
        }

        public bool UpdateSubCategory(int id, SubCategoryModel model)
        {
            using (var context = new ShivFactoryEntities())
            {
                var subCategory = context.SubCategories.FirstOrDefault(x => x.ID == id);
                if (subCategory != null)
                {
                    subCategory.SubCatName = model.SubCatName;
                    subCategory.SubCatImage = model.SubCatImage != null ? model.SubCatImage : subCategory.SubCatImage;
                    subCategory.LastUpdate = DateTime.Now;
                }
                context.SaveChanges();
                return true;
            }
        }

        public bool RemoveSubCategory(int id)
        {
            using (var context = new ShivFactoryEntities())
            {
                var subcategory = context.SubCategories.FirstOrDefault(x => x.ID == id);
                if (subcategory != null)
                {
                    context.SubCategories.Remove(subcategory);
                    return context.SaveChanges() > 0;
                }
                return false;
            }
        }

        #endregion

        #region MiniCategory
        public int SaveMiniCategory(MiniCategoryModel MinicatModel)
        {
            using (var context = new ShivFactoryEntities())
            {
                MiniCategory miniCategory = new MiniCategory()
                {
                    SubCatID = MinicatModel.SubCatID,
                    MiniCatName = MinicatModel.MiniCatName,
                    MiniCatImage = MinicatModel.MiniCatImage,
                    Adddate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    IsActive = true,
                    IsDelete = false
                };
                context.MiniCategories.Add(miniCategory);
                context.SaveChanges();
                return miniCategory.ID;
            }
        }

        public MiniCategoryModel GetMiniCategoryById(int id)
        {
            using (var context = new ShivFactoryEntities())
            {
                var result = context.MiniCategories.Where(x => x.ID == id)
                    .Select(miniCat => new MiniCategoryModel()
                    {
                        ID = miniCat.ID,
                        MiniCatName = miniCat.MiniCatName,
                        MiniCatImage = miniCat.MiniCatImage
                    }).FirstOrDefault();
                return result;
            }
        }

        public List<MiniCategoryModel> GetAllMiniCategory()
        {
            using (var context = new ShivFactoryEntities())
            {
                var result = context.MiniCategories.Where(x => x.IsDelete == false)
                    .Select(minicat => new MiniCategoryModel()
                    {
                        ID = minicat.ID,
                        MiniCatName = minicat.MiniCatName,
                        MiniCatImage = minicat.MiniCatImage,
                        Adddate = (DateTime)minicat.Adddate
                    }).ToList();
                return result;
            }
        }

        public bool UpdateMiniCategory(int id, MiniCategoryModel model)
        {
            using(var context=new ShivFactoryEntities())
            {
                var miniCategory = context.MiniCategories.FirstOrDefault(x => x.ID == id);
               if(miniCategory !=null)
                {
                    miniCategory.MiniCatName = model.MiniCatName;
                    miniCategory.MiniCatImage = model.MiniCatImage != null ? model.MiniCatImage : miniCategory.MiniCatImage;
                    miniCategory.LastUpdate = DateTime.Now;
                }
                context.SaveChanges();
                return true;
            }
        }

        public bool RemoveMiniCategory(int id)
        {
            using(var context=new ShivFactoryEntities())
            {
                var miniCat = context.MiniCategories.FirstOrDefault(x => x.ID == id);
                if(miniCat!=null)
                {
                    context.MiniCategories.Remove(miniCat);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        #endregion

        #region Brand
        public int SaveBrand(BrandModel model)
        {
            using(var context=new ShivFactoryEntities())
            {
                Brand brand = new Brand()
                {
                    BrandName=model.BrandName,
                    BrandImage=model.BrandImage,
                    Adddate=DateTime.Now,
                    LastUpdate=DateTime.Now,
                    IsActive=true,
                    IsDelete=false
                };
                context.Brands.Add(brand);
                context.SaveChanges();
                return brand.ID;
            }
        }

        public BrandModel GetBrandById(int id)
        {
            using(var context=new ShivFactoryEntities())
            {
                var result = context.Brands.Where(x => x.ID == id)
                    .Select(brand => new BrandModel
                    {
                        ID=brand.ID,
                        BrandName=brand.BrandName,
                        BrandImage=brand.BrandImage
                    }).FirstOrDefault();
                return result;
            }
        }

        public List<BrandModel> GetAllBrand()
        {
            using (var context = new ShivFactoryEntities())
            {
                var result = context.Brands.Where(x => x.IsDelete == false)
                    .Select(brand => new BrandModel()
                    {
                        ID = brand.ID,
                        BrandName = brand.BrandName,
                        BrandImage = brand.BrandImage,
                        Adddate = (DateTime)brand.Adddate
                    }).ToList();
                return result;
            }

        }

        public bool UpdateBrand(int id, BrandModel model)
        {
            using (var context = new ShivFactoryEntities())
            {
                var brand = context.Brands.FirstOrDefault(x => x.ID == id);
                if (brand != null)
                {
                    brand.BrandName = model.BrandName;
                    brand.BrandImage = model.BrandImage != null ? model.BrandImage : brand.BrandImage;
                    brand.LastUpdate = DateTime.Now;
                }
                context.SaveChanges();
                return true;
            }
        }

        public bool RemoveBrand(int id)
        {
            using(var context=new ShivFactoryEntities())
            {
                var brand = context.Brands.FirstOrDefault(X => X.ID == id);
                if(brand !=null)
                {
                    context.Brands.Remove(brand);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        #endregion

        #region Banner
        public int SaveBanner(BannerModel bnrModel)
        {
            using(var context=new ShivFactoryEntities())
            {
                Banner banner = new Banner()
                {
                    Name=bnrModel.Name,
                    BannerImage=bnrModel.BannerImage,
                    Title=bnrModel.Title,
                    Adddate=DateTime.Now,
                    LastUpdate=DateTime.Now
                };
                context.Banners.Add(banner);
                context.SaveChanges();
                return banner.ID;
            }
        }

        public BannerModel GetBannerById(int id)
        {
            using(var context=new ShivFactoryEntities())
            {
                var result = context.Banners.Where(x => x.ID == id)
                    .Select(banner => new BannerModel()
                    {
                        ID=banner.ID,
                        Name=banner.Name,
                        BannerImage=banner.BannerImage
                    }).FirstOrDefault();
                return result;
            }
        }

        public List<BannerModel> GetAllBanner()
        {
            using(var context=new ShivFactoryEntities())
            {
                var result = context.Banners.Where(x => x.IsDelete == false)
                    .Select(banner => new BannerModel()
                    {
                        ID=banner.ID,
                        Name=banner.Name,
                        BannerImage=banner.BannerImage,
                        Title=banner.Title,
                        Adddate = (DateTime)banner.Adddate
                    }).ToList();
                return result;
            }
        }

        public bool UpdateBanner(int id, BannerModel model)
        {
            using(var context=new ShivFactoryEntities())
            {
                var banner = context.Banners.FirstOrDefault(x => x.ID == id);
                if(banner !=null)
                {
                    banner.Name = model.Name;
                    banner.Title = model.Title;
                    banner.BannerImage = model.BannerImage != null ? model.BannerImage : banner.BannerImage;
                    banner.LastUpdate = DateTime.Now;
                }
                context.SaveChanges();
                return true;
            }
        }
        public bool RemoveBanner(int id)
        {
            using(var context=new ShivFactoryEntities())
            {
                var banner = context.Banners.FirstOrDefault(x => x.ID == id);
                if(banner !=null)
                {
                    context.Banners.Remove(banner);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        #endregion
    }
}




