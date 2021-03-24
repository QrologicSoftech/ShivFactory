using DataLibrary.DL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
    public class RepoAutoComplete
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        #endregion

        #region Product AutoComplete
        public List<AutoCompleteResponse> ProductAutoComplete(string searchtext)
        {
            var products = db.Products.Where(a => a.ProductName.StartsWith(searchtext) || a.Description.StartsWith(searchtext) || a.Category.CategoryName.StartsWith(searchtext) || a.SubCategory.SubCategoryName.StartsWith(searchtext) || a.MiniCategory.MiniCategoryName.StartsWith(searchtext)).Select(a => new AutoCompleteResponse()
            {
                Id = a.ProductId,
                Value = a.ProductName
            }).AsNoTracking().Take(10).ToList();

            return products;
        }

        #endregion
    }
}
