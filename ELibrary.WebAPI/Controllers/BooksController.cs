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
        private IELibraryRepository _repo;

        public BooksController(IELibraryRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<Object> Get()
        {
            
            var results = _repo.GetAllBooksWithTags()
                .OrderBy(f => f.Title)
                .Take(10)
                .ToList()
                .Select(f => new BookModel {
                    Title = f.Title,
                    Year = f.Year,
                    Author = f.Author,
                    Tags = f.Tags.Select(m=> new TagModel{
                        Name = m.Name
                    })
                });

            return results;
        }
    }
}
