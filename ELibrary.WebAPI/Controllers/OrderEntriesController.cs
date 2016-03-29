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
                                        .Select(e => TheModelFactory.Create(e));

            return results;
        }

        public HttpResponseMessage Get(DateTime orderId, int id)
        {
            var userName = _identityService.CurrentUser;
            var result = TheRepository.GetOrderEntry(userName, orderId, id);

            if (result == null)
            {
                Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, TheModelFactory.Create(result));
        }

        public HttpResponseMessage Post(DateTime orderId, [FromBody] OrderEntryModel model)
        {
            try
            {
                var entity = TheModelFactory.Parse(model);

                if (entity == null)
                    Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not read Order Entry in body");

                var order = TheRepository.GetOrder(_identityService.CurrentUser, orderId);

                if (order == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                //Make sure it is not duplicate
                if (order.Entries.Any(e => e.BookItem.Id == entity.BookItem.Id))
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Duplicate Book not allowed");

                //save new entry
                order.Entries.Add(entity);

                if (TheRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, TheModelFactory.Create(entity));
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not save to the database");
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        public HttpResponseMessage Delete (DateTime orderId, int id)
        {
            try
            {
                if(TheRepository.GetOrderEntries(_identityService.CurrentUser, orderId).Any(f=>f.Id == id)==false)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                if(TheRepository.DeleteOrderEntry(id) && TheRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
