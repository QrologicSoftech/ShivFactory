using ShivFactory.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ShivFactory.Business.Factory.Services
{
    public interface IUserService
    {
        #region SubCategory
        int SaveSubCategory(SubCategoryModel SubcatModel);
        SubCategoryModel GetSubCategoryById(int id);
        List<SubCategoryModel> GetAllSubCategory();
        bool UpdateSubCategory(int id, SubCategoryModel model);
        bool RemoveSubCategory(int id);

        #endregion

        #region MiniCategory
        int SaveMiniCategory(MiniCategoryModel MinicatModel);
        MiniCategoryModel GetMiniCategoryById(int id);
        List<MiniCategoryModel> GetAllMiniCategory();
        bool UpdateMiniCategory(int id, MiniCategoryModel model);
        bool RemoveMiniCategory(int id);

        #endregion

        #region Brand
        int SaveBrand(BrandModel brndModel);
        BrandModel GetBrandById(int id);
        List<BrandModel> GetAllBrand();
        bool UpdateBrand(int id, BrandModel model);
        bool RemoveBrand(int id);

        #endregion

        #region Banner
        int SaveBanner(BannerModel model);
        BannerModel GetBannerById(int id);
        List<BannerModel> GetAllBanner();
        bool UpdateBanner(int id, BannerModel model);
        bool RemoveBanner(int id);

        #endregion
    }
}
