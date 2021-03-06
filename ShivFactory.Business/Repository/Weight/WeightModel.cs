﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
  public  class WeightModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Weight Name Required")]
        public string Name { get; set; }
    }
}
