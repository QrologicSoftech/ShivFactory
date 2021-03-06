using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace ShivFactory.Business.Repository
{
    public class RepoCommon
    {

        public string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        public string SaveImage(HttpPostedFileBase PostedFile)
        {
            string path = WebConfigurationManager.AppSettings["mainPath"].ToString();
            string fullPath = HttpContext.Current.Server.MapPath(path);
         
            string guid = DateTime.Now.ToString("yyyyMMddHHmmssffff");
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
                    //Guid guid = Guid.NewGuid();
                    string guid = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    string filename = guid + Path.GetFileName(file.FileName);
                    file.SaveAs(fullPath + filename);
                    string folderpath = path + filename;
                    imagePathlist.Add(folderpath);
                }
                else { imagePathlist.Add("");  }
            }
            return imagePathlist;
        }

        public string  checkfile(string userimgpath)
        {
            try
            {
                if (string.IsNullOrEmpty(userimgpath)) { return "/Content/UploadedImages/Images/NoImg.png"; }
                string path = WebConfigurationManager.AppSettings["mainPath"].ToString();
                string fullPath = HttpContext.Current.Server.MapPath(path);
                string[] image = userimgpath.Split('/');
                //find and  delete image 
                if (System.IO.File.Exists(fullPath+image[3]))
                {
                    return userimgpath;
                }
                else
                {
                    return "/Content/UploadedImages/Images/NoImg.png";
                }
            }
            catch (Exception e)
            {
                return "/Content/UploadedImages/Images/NoImg.png";
            }
        }

        public bool deletefile(string userimgpath)
        {
            try
            {
                string path = WebConfigurationManager.AppSettings["mainPath"].ToString();
                string fullPath = HttpContext.Current.Server.MapPath(path);
                string[] image = userimgpath.Split('/');
                System.IO.File.Delete(fullPath+image);
                    return true;
                }
            catch(Exception e)
            {
                return false;
            }
            

        }

        #region Call SMS OTP API 
        public string sendOtpSMS(string mobilenumber)
        { // put in try when sms
            Random generator = new Random();
            int r = generator.Next(100000, 1000000);
            return "123456"; 
        }

        public bool VerifyOTP(string code)
        {
            try {
                return true;
            }
            catch (Exception e)
            {
                return false; 
            }
            
        }
        #endregion
    }
}
