using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
   public  class UserProfileResponse
    {
		public string UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Gender { get; set; }
		public string Email { get; set; }
		public string Mobile { get; set; }
		public string Password { get; set; }
		public string UserImage { get; set; }
		public string Address { get; set; }
		public string LastLoginIP { get; set; }
		public DateTime LastLoginDate { get; set; }
		public DateTime LastUpdate { get; set; }
		public bool IsActive { get; set; }
		public bool IsDelete { get; set; }
	}
}
