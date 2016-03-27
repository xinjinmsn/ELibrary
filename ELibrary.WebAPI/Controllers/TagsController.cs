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

        public TagModel Get(int id)
        {
            var result = TheRepository.GetTag(id);

            if (result != null)
                return TheModelFactory.Create(result);

            return null;
        }
    }
}
