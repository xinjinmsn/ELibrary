using ELibrary.Data;
using ELibrary.WebAPI.Models;
using ELibrary.WebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ELibrary.WebAPI.Controllers
{
    public class OrderEntriesController : BaseApiController
    {
        private IELibraryIdentityService _identityService;

        public OrderEntriesController(IELibraryRepository repo, IELibraryIdentityService identityService)
            : base(repo)
        {
            _identityService = identityService;
        }

        public IEnumerable<OrderEntryModel> Get(DateTime orderId)
        {
            var userName = _identityService.CurrentUser;
            var results = TheRepository.GetOrderEntries(userName, orderId)
                                        .ToList()
                                        .Select(e=>TheModelFactory.Create(e));

            return results;
        }

        public HttpResponseMessage Get(DateTime orderId, int id)
        {
            var userName = _identityService.CurrentUser;
            var result = TheRepository.GetOrderEntry(userName, orderId, id);

            if(result==null)
            {
                Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, TheModelFactory.Create(result));
        }
    }
}
