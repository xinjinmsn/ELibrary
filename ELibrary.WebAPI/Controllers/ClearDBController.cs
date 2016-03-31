using ELibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ELibrary.WebAPI.Controllers
{
    public class ClearDBController : BaseApiController
    {
        public ClearDBController(IELibraryRepository repo) : base(repo)
        {
        }

        public HttpResponseMessage Get()
        {
            try
            {
                TheRepository.ClearDB();
                return Request.CreateResponse(HttpStatusCode.OK, "Succeed!");
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
