using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibrary.WebAPI.Services
{
    public class UserService : IUserService
    {
        public bool Authenticate(string userName, string password)
        {
            if (userName.Equals("testuser", StringComparison.OrdinalIgnoreCase) && password == "Password01!")
                return true;
            else
                return false;
        }
    }
}