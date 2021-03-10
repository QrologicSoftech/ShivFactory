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

        #region save Image from http posted
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
                else { imagePathlist.Add(""); }
            }
            return imagePathlist;
        }
        #endregion
        #region check file exist or not 
        public string checkfile(string userimgpath)
        {
            try
            {
                if (string.IsNullOrEmpty(userimgpath)) { return "/Content/UploadedImages/Images/NoImg.png"; }
                string path = WebConfigurationManager.AppSettings["mainPath"].ToString();
                string fullPath = HttpContext.Current.Server.MapPath(path);
                string[] image = userimgpath.Split('/');
                //find and  delete image 
                if (System.IO.File.Exists(fullPath + image[3]))
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

        #endregion check file 
        #region delete file from path 
        public bool deletefile(string userimgpath)
        {
            try
            {
                var filePath = HttpContext.Current.Server.MapPath(userimgpath);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }


        }
        #endregion

        #region Call SMS OTP API 
        public string sendOtpSMS(string mobilenumber)
        { // put in try when sms
            Random generator = new Random();
            int r = generator.Next(100000, 1000000);
            return "123456";
        }

        public bool VerifyOTP(string code)
        {
            try
            {
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        #endregion

        #region Convert Base64 to image 
        public string Base64ToImage(string base64String)
        {
            string[] strings = base64String.Split(',');
            string extension;
            switch (strings[0])
            {//check image's extension
                case "data:image/jpeg;base64":
                    extension = "jpeg";
                    break;
                case "data:image/png;base64":
                    extension = "png";
                    break;
                default://should write cases for more images types
                    extension = "jpg";
                    break;
            }
            byte[] imageBytes = Convert.FromBase64String(strings[1]);

            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);

            string path = WebConfigurationManager.AppSettings["mainPath"].ToString();
            string fullPath = HttpContext.Current.Server.MapPath(path);

            string guid = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "." + extension;
            image.Save(fullPath + guid);
            return path + guid;
            // return image;
            //image.Save("sss",Ima)
        }
        #endregion
    }
}
