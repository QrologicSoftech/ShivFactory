﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ShivFactory.Business.Repository.Product
{
    public class ProductModel
    {
		public int ProductId { get; set; }
		[Required(ErrorMessage = "Product Name required")]
		public string ProductName { get; set; }
		[Required(ErrorMessage = "Product Description required")]
		[MaxLength(100, ErrorMessage = "Description cannot be longer than 100 characters.")]
		
		public string Description { get; set; }
		[Required(ErrorMessage = "Sale Price Required")]
		public decimal SalePrice { get; set; }
		[Required(ErrorMessage = "List Price Required")]
		public decimal ListPrice { get; set; }
		public string ImagePath { get; set; }
		
		[Required(ErrorMessage = "Please select category")]
		public int Category { get; set; }
		[Required(ErrorMessage = "Please select Sub category")]
		public int SubCategory { get; set; }
		[Required(ErrorMessage = "Please select Mini category")]
		public int MiniCategory { get; set; }
		public bool IsActive { get; set; }

		public DateTime AddUpdate { get; set; }
		public DateTime LastUpdate { get; set; }
		public int StockCount { get; set; }
		public DateTime MgfDate { get; set; }
		public string ShellLife { get; set; }
		public string ProductWarning { get; set; }
		public string MgfDetail { get; set; }
		public decimal localshipingcharge { get; set; }
		public decimal zonalshipingcharge { get; set; }
		public decimal nationalshippingcharge { get; set; }
		public decimal Length { get; set; }
		public decimal Breadth { get; set; }
		public decimal Height { get; set; }
		public string InactiveReason { get; set; }

		public List<string> imgPathList { get; set;  }
		public HttpPostedFileBase  PostedFile { get; set; }

		public List<HttpPostedFileBase> files { get; set; }
	}
}
