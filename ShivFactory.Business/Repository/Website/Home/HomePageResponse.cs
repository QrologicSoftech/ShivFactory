using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository.Website
{
    public class HomePageResponse
    {
        public List<HomeCategoryResponse> Products { get; set; }
        public List<Images> Banners { get; set; }

        public CartModel cartModel { get; set;  }
    }

    public class HomeCategoryResponse
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public List<Images> SubCategory { get; set; }
        
    }
    public class Images
    {
        public string ImagePath { get; set; }
        public int SubCategoryId { get; set; }
        public string price { get; set; }
        public string SubCategoryName { get; set; }
        public string ListPrice { get; set; }
    }
}
