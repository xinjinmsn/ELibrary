using ELibrary.Data;
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
        private IELibraryRepository _repo;

        public ModelFactory(HttpRequestMessage request, IELibraryRepository repo)
        {
            _urlHelp = new UrlHelper(request);
            _repo = repo;
        }
        public BookModel Create(Book book, bool withTags = true)
        {
            if(withTags)
                return new BookWithTagsModel
                {
                    Url = _urlHelp.Link("Book", new { bookid=book.Id}),
                    Title = book.Title,
                    Description = book.Description,
                    Year = book.Year,
                    Author = book.Author,
                    Tags = book.Tags.Select(m => Create(m, false))
                };
            else
                return new BookModel
                {
                    Url = _urlHelp.Link("Book", new { bookid = book.Id }),
                    Title = book.Title,
                    Description = book.Description,
                    Year = book.Year,
                    Author = book.Author,
                };

        }

        public OrderSummaryModel CreateSummary(Order order)
        {
            return new OrderSummaryModel
            {
                OrderDate = order.CurrentDate,
                CostSum = order.Entries.Sum(f => f.BookItem.Price * f.Quantity),
                TotalCount = order.Entries.Sum(f =>f.Quantity)
                
            };
        }

        public OrderEntryModel Create(OrderEntry entry)
        {
            return new OrderEntryModel
            {
                Url = _urlHelp.Link("OrderEntries", new { orderid = entry.Order.CurrentDate.ToString("yyyy-MM-dd"), id = entry.Id }),
                Quantity = entry.Quantity,
                BookTitle = entry.BookItem.Title,
                BookUrl = _urlHelp.Link("Book", new { bookid = entry.BookItem.Id})
            };
        }

        public OrderEntry Parse(OrderEntryModel model)
        {
            try
            {
                var entry = new OrderEntry();
                if(model.Quantity!=default(int))
                {
                    entry.Quantity = model.Quantity;
                }

                if(!string.IsNullOrWhiteSpace(model.BookUrl))
                {
                    var uri = new Uri(model.BookUrl);
                    var bookId = int.Parse(uri.Segments.Last());
                    var book = _repo.GetBook(bookId);
                    entry.BookItem = book;

                    return entry;
                }

                return entry;

            }
            catch
            {
                return null;
            }
        }

        public OrderModel Create(Order d)
        {
            return new OrderModel
            {
                Url = _urlHelp.Link("Orders", new { orderid = d.CurrentDate.ToString("yyyy-MM-dd") }),
                CurrentDate = d.CurrentDate,
                Entries = d.Entries.Select(e=>Create(e))
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