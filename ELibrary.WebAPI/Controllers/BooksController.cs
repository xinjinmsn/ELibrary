using ELibrary.Data;
using ELibrary.Data.Entities;
using ELibrary.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using ELibrary.WebAPI.Filters;

namespace ELibrary.WebAPI.Controllers
{
    public class BooksController : BaseApiController
    {
        
        public BooksController(IELibraryRepository repo) : base(repo)
        {
        }

        const int PAGE_SIZE = 5;
        public object Get(bool includeTags=true, int page=0)
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

            var baseQuery = query.OrderBy(f => f.Title);

            var totalCount = baseQuery.Count();
            var totalPages = Math.Ceiling((double)totalCount/PAGE_SIZE);
            
            var helper = new UrlHelper(Request);

            var links = new List<LinkModel>();

            if(page>0)
            {
                links.Add(TheModelFactory.CreateLink(helper.Link("Book", new { page = page - 1 }), "prevPage"));
            }

            if(page < totalPages - 1)
            {
                links.Add(TheModelFactory.CreateLink(helper.Link("Book", new { page = page + 1 }), "nextPage"));
            }

            var results = baseQuery.Skip(PAGE_SIZE*page)
                            .Take(PAGE_SIZE)
                            .ToList()
                            .Select(f => (BookWithTagsModel)TheModelFactory.Create(f));

            return new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                Links = links,
                Results = results
            };
        }

        public BookWithTagsModel Get(int bookid)
        {
            return (BookWithTagsModel)TheModelFactory.Create(TheRepository.GetBook(bookid));
        }
    }
}
