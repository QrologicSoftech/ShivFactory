using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShivFactory.Business.Models.Other
{
    public class Constrant
    {
    }
    public class UserRoles
    {
        public static string SuperAdmin = "SuperAdmin";
        public static string Admin = "Admin";
        public static string Vendor = "Vendor";
        public static string Customer = "Customer";
    }
    public class CookieName
    {
        public static string UserName = "ShivaUserName";
        public static string UserId = "ShivaUserId";
        public static string Role = "ShivaRole";
        public static string FirstName = "ShivaFirstName";
        public static string LastName = "ShivaLastName";
        public static string Mobile = "ShivaMobile";
        public static string EmailId = "ShivaEmailId";
        public static string TempOrderId = "ShivaTempOrderId";
    }
}