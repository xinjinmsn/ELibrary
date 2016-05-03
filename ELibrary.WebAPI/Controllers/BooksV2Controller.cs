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
    public class BooksV2Controller : BaseApiController
    {

        public BooksV2Controller(IELibraryRepository repo) : base(repo)
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
            var prevUrl = page > 0 ? helper.Link("Book", new { page=page-1}): "";
            var nextUrl = page < totalPages - 1 ? helper.Link("Book", new { page = page + 1}): "";

            var results = baseQuery.Skip(PAGE_SIZE*page)
                            .Take(PAGE_SIZE)
                            .ToList()
                            .Select(f => (BookWithTagsV2Model)TheModelFactory.Create2(f));

            return new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PrevPageUrl = prevUrl,
                NextPageUrl = nextUrl,
                Results = results
            };
        }

        public BookWithTagsV2Model Get(int bookid)
        {
            return (BookWithTagsV2Model)TheModelFactory.Create2(TheRepository.GetBook(bookid));
        }
    }
}
