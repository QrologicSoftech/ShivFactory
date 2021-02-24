using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository.Product
{
    public class ProductModel
    {
		public int ProductId { get; set; }
		[Required(ErrorMessage = "Product Name required")]
		public string ProductName { get; set; }

		[Required(ErrorMessage = "Product Description required")]
		public string Description { get; set; }
		[MaxLength(100, ErrorMessage = "Description cannot be longer than 100 characters.")]
		[Required(ErrorMessage = "Sale Price Required")]
		public decimal SalePrice { get; set; }
		[Required(ErrorMessage = "List Price Required")]
		public decimal ListPrice { get; set; }
		[Required(ErrorMessage = " Image Required")]
		public string MainImage { get; set; }
		public string Image1 { get; set; }
		public string Image2 { get; set; }
		public string Image3 { get; set; }
		public string Image4 { get; set; }
		public string Image5 { get; set; }
		public string Image6 { get; set; }
		[Required(ErrorMessage = "Please select category")]
		public int Category { get; set; }
		[Required(ErrorMessage = "Please select Sub category")]
		public int SubCategory { get; set; }
		[Required(ErrorMessage = "Please select Mini category")]
		public int MiniCategory { get; set; }
	}
}
