using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace ShivFactory.Business.Repository.Common
{
    public class RepoCommon
    {
        public string SaveImage(HttpPostedFileBase PostedFile)
        {
            string path = WebConfigurationManager.AppSettings["mainPath"].ToString();
            string fullPath = HttpContext.Current.Server.MapPath(path);
            Guid guid = Guid.NewGuid();
            string filename = guid + Path.GetFileName(PostedFile.FileName);
            PostedFile.SaveAs(fullPath + filename);
            return path + filename;
        }
    }
}
