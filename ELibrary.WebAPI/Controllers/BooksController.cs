using ELibrary.Data;
using ELibrary.Data.Entities;
using ELibrary.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace ELibrary.WebAPI.Controllers
{
    public class BooksController : ApiController
    {
        private ModelFactory _modelFactory;
        private IELibraryRepository _repo;

        public BooksController(IELibraryRepository repo)
        {
            _repo = repo;
            _modelFactory = new ModelFactory();
        }
        public IEnumerable<BookModel> Get()
        {
            
            var results = _repo.GetAllBooksWithTags()
                .OrderBy(f => f.Title)
                .Take(10)
                .ToList()
                .Select(f => _modelFactory.Create(f));

            return results;
        }

        public BookModel Get(int id)
        {
            return _modelFactory.Create(_repo.GetBook(id));
        }
    }
}
