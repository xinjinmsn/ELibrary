using ELibrary.Data;
using ELibrary.Data.Entities;
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
        public IEnumerable<Book> Get()
        {
            var repo = new ELibraryRepository(new ELibraryContext());

            var results = repo.GetAllBooks()
                .OrderBy(f => f.Title)
                .Take(10)
                .ToList();

            return results;
        }
    }
}
