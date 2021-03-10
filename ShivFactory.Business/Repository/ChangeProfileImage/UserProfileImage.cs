using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository.ChangeProfileImage
{
  public class UserProfileImage
    {
        public string UserId { get; set;  }        
        public string ImagePath { get; set; }
        [Required(ErrorMessage="Choosen Image is manadatory")]
        public System.Web.HttpPostedFileBase PostedFile { get; set; }
        public string ReturnUrl { get; set; }

    }
}
