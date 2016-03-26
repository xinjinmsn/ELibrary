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
    public class TagsController : ApiController
    {
        private ModelFactory _modelFactory;
        private IELibraryRepository _repo;

        public TagsController(IELibraryRepository repo)
        {
            _repo = repo;
            _modelFactory = new ModelFactory();
        }

        public IEnumerable<TagModel> Get(int bookid)
        {
            var results = _repo.GetTagsForBook(bookid)
                .ToList()
                .Select(f => _modelFactory.Create(f));

            return results;
        }

        public TagModel Get(int bookid, int id)
        {
            var result = _repo.GetTag(bookid, id);

            if (result != null)
                return _modelFactory.Create(result);

            return null;
        }


    }
}
