using ELibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibrary.WebAPI.Models
{
    public class ModelFactory
    {
        public BookModel Create(Book book)
        {
            return new BookModel
            {
                Url = string.Format("http://localhost:9294/api/library/books/{0}", book.Id),
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
                Url = "",
                Name = tag.Name
            };
        }
    }
}