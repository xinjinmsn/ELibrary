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
    public class BooksController : BaseApiController
    {
        public BooksController(IELibraryRepository repo) : base(repo)
        {
        }
        public IEnumerable<BookWithTagsModel> Get(bool includeTags=true)
        {
            IQueryable<Book> query;

            if(includeTags)
            {
                query = TheRepository.GetAllBooksWithTags();
            }
            else
            {
                query = TheRepository.GetAllBooks(); 
            }

            var results = query.OrderBy(f => f.Title)
                            .Take(10)
                            .ToList()
                            .Select(f => TheModelFactory.Create(f));
            return results;
        }

        public BookWithTagsModel Get(int bookid)
        {
            return TheModelFactory.Create(TheRepository.GetBook(bookid));
        }
    }
}
