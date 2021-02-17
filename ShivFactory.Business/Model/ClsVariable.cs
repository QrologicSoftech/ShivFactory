using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace ShivFactory.Business.Model
{
    public class ClsVariable
    {
        public static string ImgCategory = WebConfigurationManager.AppSettings["mainPath"] + "Category/";
    }
}
