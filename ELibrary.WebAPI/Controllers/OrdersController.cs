using ELibrary.Data;
using ELibrary.WebAPI.Services;
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
        private IELibraryIdentityService _identityService;

        public OrdersController(IELibraryRepository repo, IELibraryIdentityService identityService)
            :base(repo)
        {
            _identityService = identityService;
        }

    }
}
