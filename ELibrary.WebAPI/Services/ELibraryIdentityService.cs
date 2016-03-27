using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibrary.WebAPI.Services
{
    public class ELibraryIdentityService : IELibraryIdentityService
    {
        public string CurrentUser
        {
            get
            {
                return "testuser";
            }
        }
    }
}