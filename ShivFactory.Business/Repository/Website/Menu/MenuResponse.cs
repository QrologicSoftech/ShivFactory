using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository.Website
{
    public class MenuModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MenuModel> MiniCategory { get; set; }
    }
    public class MenuResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public List<MenuModel> SubCategory { get; set; }

    }
}
