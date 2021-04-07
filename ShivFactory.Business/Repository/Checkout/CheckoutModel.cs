using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
    public class CheckoutModel
    {

        //#region order
        //public int OrderId { get; set; }
        //#endregion

        //#region payment
        //#endregion
        public List<DeliveryAddress> DeliveryAddress { get; set; }

        public List<CartModel> CartModel { get; set; }
    }

    public class DeliveryAddress
    {

        public int ID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Pincode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int userDetailId { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
       

    }
}
