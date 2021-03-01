﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
    public class BrandResponse
    {
        public int SrNo { get; set; }
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string BrandName { get; set; }
        public bool IsActive { get; set; }
        public string AddDate { get; set; }
        public string CategoryName { get; set; }
    }
}
