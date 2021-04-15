using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
    public class AddToCart
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int ProductVarientID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int TempOrderId { get; set; }
        public decimal NetAmt { get; set; }
        public bool IsUserWishList { get; set; }
        public int vendorId { get; set; }
        public string ImagePath { get; set; }
        public decimal ListPrice { get; set; }
        public decimal OfferPercentage { get; set; }
    }
    public class CartModel
    {
        public decimal CartValue { get; set; }
        public List<AddToCart> CartItems { get; set; }
    }

    public class UpdateCart
     {
    public int TempOrderDetailId { get; set; }
    public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
