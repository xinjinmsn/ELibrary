using ELibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace ELibrary.WebAPI.Models
{
    public class ModelFactory
    {
        private UrlHelper _urlHelp;

        public ModelFactory(HttpRequestMessage request)
        {
            _urlHelp = new UrlHelper(request);
        }
        public BookWithTagsModel Create(Book book)
        {
            return new BookWithTagsModel
            {
                Url = _urlHelp.Link("Book", new { bookid=book.Id}),
                Title = book.Title,
                Year = book.Year,
                Author = book.Author,
                Tags = book.Tags.Select(m => Create(m))
            };
        }

        public TagModel Create(Tag tag)
        {
            return new TagModel
            {
                Url = _urlHelp.Link("tags", new { id=tag.Id}),
                Name = tag.Name
            };
        }
    }
}