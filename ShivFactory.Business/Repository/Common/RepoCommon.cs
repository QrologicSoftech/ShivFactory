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

        public List<string> SaveProductMultipleImage(List<HttpPostedFileBase> postedFilelist)
        {
            List<string> imagePathlist = new List<string>();
            string path = WebConfigurationManager.AppSettings["mainPath"].ToString();
            string fullPath = HttpContext.Current.Server.MapPath(path);
            foreach (var file in postedFilelist)
            {
                if (file != null)
                {
                    Guid guid = Guid.NewGuid();
                    string filename = guid + Path.GetFileName(file.FileName);
                    file.SaveAs(fullPath + filename);
                    string folderpath = path + filename;
                    imagePathlist.Add(folderpath);
                }
                else { imagePathlist.Add("");  }
            }
            return imagePathlist;
        }

        public bool checkfile(string userimgpath)
        {
            try
            {
            //find and  delete image 
            if (!System.IO.File.Exists(userimgpath))
            {
                return false;
            }
            return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool deletefile(string userimgpath)
        {
            try
            {
                //string path = WebConfigurationManager.AppSettings["mainPath"].ToString();
                //string fullPath = HttpContext.Current.Server.MapPath(path);
                //find and  delete image 

                System.IO.File.Delete(userimgpath);
                    return true;
                }
            catch(Exception e)
            {
                return false;
            }
            

        }
    }
}
