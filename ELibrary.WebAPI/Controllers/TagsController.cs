using ELibrary.Data;
using ELibrary.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ELibrary.WebAPI.Controllers
{
    public class TagsController : BaseApiController
    {
        public TagsController(IELibraryRepository repo): base(repo)
        {

        }

        public HttpResponseMessage Get(int id)
        {
            var result = TheRepository.GetTag(id);

            if (result != null)
                return Request.CreateResponse(HttpStatusCode.OK, (TagWithBooksModel)TheModelFactory.Create(result));

            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        public IEnumerable<TagModel> Get()
        {
            var results = TheRepository.GetAllTags()
                                        .ToList()
                                        .Select(e=>TheModelFactory.Create(e, false));

            return results;
        }
    }
}
