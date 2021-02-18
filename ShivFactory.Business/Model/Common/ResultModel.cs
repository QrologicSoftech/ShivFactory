using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Model
{
    public class ResultModel
    {
        public bool ResultFlag { get; set; }
        public dynamic Data { get; set; }
        public string Message { get; set; }
    }
}
