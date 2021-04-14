using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
    public class CheckoutModel
    {

        
        public List<DeliveryAddress> DeliveryAddress { get; set; }

        public List<CartModel> CartModel { get; set; }
    }

   
}
