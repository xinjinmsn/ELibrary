using ELibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ELibrary.WebAPI.Controllers
{
    public class OrdersController : BaseApiController
    {
        public OrdersController(IELibraryRepository repo)
            :base(repo)
        {

        }

    }
}
