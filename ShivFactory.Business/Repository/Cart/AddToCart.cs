﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
   public class AddToCart
    {
        public int ProductID { get; set; }
        public int ProductVarientID { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public string Brand { get; set; }
        public string Varient { get; set; }
    }
}
