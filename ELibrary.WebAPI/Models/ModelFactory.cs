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
        public BookModel Create(Book book, bool withTags = true)
        {
            if(withTags)
                return new BookWithTagsModel
                {
                    Url = _urlHelp.Link("Book", new { bookid=book.Id}),
                    Title = book.Title,
                    Year = book.Year,
                    Author = book.Author,
                    Tags = book.Tags.Select(m => Create(m, false))
                };
            else
                return new BookModel
                {
                    Url = _urlHelp.Link("Book", new { bookid = book.Id }),
                    Title = book.Title,
                    Year = book.Year,
                    Author = book.Author,
                };

        }

        public OrderModel Create(Order d)
        {
            return new OrderModel
            {
                Url = _urlHelp.Link("Orders", new { orderid = d.CurrentDate.ToString("yyyy-MM-dd") }),
                CurrentDate = d.CurrentDate
        };
    }

        public TagModel Create(Tag tag, bool withBook = true)
        {
            if(withBook)
                return new TagWithBooksModel
                {
                    Url = _urlHelp.Link("tags", new { id = tag.Id }),
                    Name = tag.Name,
                    Books = tag.Books.Select(m=>Create(m, false))
                };
            else
                return new TagModel
                {
                    Url = _urlHelp.Link("tags", new { id = tag.Id }),
                    Name = tag.Name,
                };


        }

    }
}