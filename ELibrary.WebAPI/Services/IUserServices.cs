using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.WebAPI.Services
{
    public interface IUserService
    {
        bool Authenticate(string userName, string password);
    }
}
