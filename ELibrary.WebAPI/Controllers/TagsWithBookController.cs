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
    public class TagsWithBookController : BaseApiController
    {
        public TagsWithBookController(IELibraryRepository repo):base(repo)
        {

        }

        public IEnumerable<TagModel> Get(int bookid)
        {
            var results = TheRepository.GetTagsForBook(bookid)
                .ToList()
                .Select(f => TheModelFactory.Create(f, false));

            return results;
        }

        public TagModel Get(int bookid, int id)
        {
            var result = TheRepository.GetTag(bookid, id);

            if (result != null)
                return TheModelFactory.Create(result, false);

            return null;
        }




    }
}
