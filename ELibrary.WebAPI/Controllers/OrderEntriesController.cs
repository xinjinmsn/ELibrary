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

        public IHttpActionResult Get(DateTime orderId)
        {
            var userName = _identityService.CurrentUser;
            var results = TheRepository.GetOrderEntries(userName, orderId)
                                        .ToList()
                                        .Select(e => TheModelFactory.Create(e));

            return Ok(results);
        }

        public IHttpActionResult Get(DateTime orderId, int id)
        {
            var userName = _identityService.CurrentUser;
            var result = TheRepository.GetOrderEntry(userName, orderId, id);

            if (result == null)
            {
                NotFound();
            }

            return Ok(TheModelFactory.Create(result));
        }

        public IHttpActionResult Post(DateTime orderId, [FromBody] OrderEntryModel model)
        {
            try
            {
                var entity = TheModelFactory.Parse(model);

                if (entity == null)
                    Content(HttpStatusCode.NotFound, "Could not read Order Entry in body");

                var order = TheRepository.GetOrder(_identityService.CurrentUser, orderId);

                if (order == null)
                    return NotFound();

                //Make sure it is not duplicate
                if (order.Entries.Any(e => e.BookItem.Id == entity.BookItem.Id))
                    return Content(HttpStatusCode.BadRequest, "Duplicate Book not allowed");

                //save new entry
                order.Entries.Add(entity);

                if (TheRepository.SaveAll())
                {
                    return Content(HttpStatusCode.Created, TheModelFactory.Create(entity));
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Could not save to the database");
                }

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }

        }

        public IHttpActionResult Delete (DateTime orderId, int id)
        {
            try
            {
                if(TheRepository.GetOrderEntries(_identityService.CurrentUser, orderId).Any(f=>f.Id == id)==false)
                {
                    return NotFound();
                }

                if(TheRepository.DeleteOrderEntry(id) && TheRepository.SaveAll())
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }

            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        public IHttpActionResult Patch(DateTime orderId, int id, [FromBody] OrderEntryModel model)
        {
            try
            {
                var entity = TheRepository.GetOrderEntry(_identityService.CurrentUser, orderId, id);
                if (entity == null)
                    return NotFound();

                var parsedValue = TheModelFactory.Parse(model);
                if (parsedValue == null)
                    return BadRequest();

                if(entity.Quantity != model.Quantity)
                {
                    entity.Quantity = model.Quantity;
                    if (TheRepository.SaveAll())
                        return Ok();
                }

                return BadRequest();
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
