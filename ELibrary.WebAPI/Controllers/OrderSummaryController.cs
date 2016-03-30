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
    public class OrderSummaryController : BaseApiController
    {
        private IELibraryIdentityService _identityService;

        public OrderSummaryController(IELibraryRepository repo, IELibraryIdentityService identityService)
            :base(repo)
        {
            _identityService = identityService;
        }

        public HttpResponseMessage Get(DateTime orderId)
        {
            try
            {
                var order = TheRepository.GetOrder(_identityService.CurrentUser, orderId);

                if(order==null)
                {
                    Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, TheModelFactory.CreateSummary(order));


            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
