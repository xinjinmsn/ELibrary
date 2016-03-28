﻿//#define TEST_SEED
#define FORCE_RECREATE

using ELibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Data
{
    public class ELibrarySeeder
    {
        private ELibraryContext _ctx;

        public ELibrarySeeder(ELibraryContext ctx)
        {
            _ctx = ctx;
        }

        public void Seed()
        {
#if !(TEST_SEED || FORCE_RECREATE)
            if (_ctx.Authors.Count() > 0)
                return;

#endif

#if TEST_SEED || FORCE_RECREATE
      ExecuteQueries(
        "DELETE FROM TagBooks",
        "DELETE FROM [Library].[OrderEntry]",
        "DELETE FROM Book.Tag",
        "DELETE FROM Book.Book",
        "DELETE FROM Book.Author",
        "DELETE FROM [Library].[Order]"
      );
#endif


            SeedTags();
            SeedBooks();
            SeedOrders();
            SeedOrderEntries();


        }

        void SeedTags()
        {
            _ctx.Tags.AddOrUpdate(x => x.Name,
                new Tag { Name = "Programming" },
                new Tag { Name = "C#" },
                new Tag { Name = ".Net" },
                new Tag { Name = "Software Design & Engineering" },
                new Tag { Name = "Enterprise Applications" },
                new Tag { Name = "Web Design" },
                new Tag { Name = "JavaScript"}
                );

            _ctx.SaveChanges();
        }

        void SeedOrders()
        {
            _ctx.Orders.AddOrUpdate(x=>x.CurrentDate,
                new Order { UserName="testuser", CurrentDate=new DateTime(2016, 1, 3) }
                );
            _ctx.SaveChanges();
        }

        void SeedBooks()
        {

            //var book = new Book
            //{
            //    Title = "CLR via C#, Second Edition",
            //    Author = new Author { Name = "Jeffrey Richter" },
            //    Pages = 736,
            //    Price = 39.99m,
            //    Stock = 9,
            //    Year = 2006,
            //};

            //var tag = _ctx.Tags.Where(f => f.Name == "Programming").FirstOrDefault();

            //tag.Books.Add(book);
            //book.Tags.Add(tag);



            //Add book CLR via C#, Second Edition
            var book = new Book
            {
                Title = "CLR via C#, Second Edition",
                Author = new Author { Name = "Jeffrey Richter" },
                Pages = 736,
                Price = 39.99m,
                Stock = 9,
                Year = 2006,
            };

            var tag = _ctx.Tags.Where(f => f.Name == "Programming").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            tag = _ctx.Tags.Where(f => f.Name == "C#").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            tag = _ctx.Tags.Where(f => f.Name == ".Net").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            _ctx.Books.Add(book);


            //Add book Code Complete: A Practical Handbook of Software Construction, Second Edition
            book = new Book
            {
                Title = "Code Complete: A Practical Handbook of Software Construction, Second Edition",
                Author = new Author { Name = "Steve McConnell" },
                Pages = 960,
                Price = 34.11m,
                Stock = 7,
                Year = 2004,
            };

            tag = _ctx.Tags.Where(f => f.Name == "Software Design & Engineering").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            tag = _ctx.Tags.Where(f => f.Name == "Enterprise Applications").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            _ctx.Books.Add(book);


            //Add book Web Design with HTML, CSS, JavaScript and jQuery Set
            book = new Book
            {
                Title = "Web Design with HTML, CSS, JavaScript and jQuery Set",
                Author = new Author { Name = "Jon Duckett" },
                Pages = 1152,
                Price = 33.95m,
                Stock = 7,
                Year = 2014,
            };

            tag = _ctx.Tags.Where(f => f.Name == "Programming").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            tag = _ctx.Tags.Where(f => f.Name == "Web Design").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            _ctx.Books.Add(book);

            //Add book JavaScript: The Good Parts
            book = new Book
            {
                Title = "JavaScript: The Good Parts",
                Author = new Author { Name = "Douglas Crockford" },
                Pages = 176,
                Price = 20.81m,
                Stock = 11,
                Year = 2008,
            };

            tag = _ctx.Tags.Where(f => f.Name == "Programming").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            tag = _ctx.Tags.Where(f => f.Name == "Web Design").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            tag = _ctx.Tags.Where(f => f.Name == "JavaScript").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            _ctx.Books.Add(book);

            //Add book Clean Code: A Handbook of Agile Software Craftsmanship
            book = new Book
            {
                Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
                Author = new Author { Name = "Robert C. Martin" },
                Pages = 464,
                Price = 33.53m,
                Stock = 5,
                Year = 2008,
            };

            tag = _ctx.Tags.Where(f => f.Name == "Software Design & Engineering").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            _ctx.Books.Add(book);

            //Save all changes
            _ctx.SaveChanges();

        }


        void SeedOrderEntries()
        {
            var order = _ctx.Orders.Where(f => f.UserName == "testuser" && f.CurrentDate == new DateTime(2016, 1, 3)).FirstOrDefault(); 

            var entry = new OrderEntry
            {
                Quantity = 1
            };
            entry.BookItem = _ctx.Books.Where(f => f.Title == "JavaScript: The Good Parts").FirstOrDefault();
            entry.Order = order;

            _ctx.OrderEntries.Add(entry);

            entry = new OrderEntry
            {
                Quantity = 1
            };
            entry.BookItem = _ctx.Books.Where(f => f.Title == "Web Design with HTML, CSS, JavaScript and jQuery Set").FirstOrDefault();
            entry.Order = order;

            _ctx.OrderEntries.Add(entry);

            entry = new OrderEntry
            {
                Quantity = 1
            };
            entry.BookItem = _ctx.Books.Where(f => f.Title == "CLR via C#, Second Edition").FirstOrDefault();
            entry.Order = order;

            _ctx.OrderEntries.Add(entry);


            _ctx.SaveChanges();
        }

        void ExecuteQueries(params string[] sqlStatements)
        {
            foreach (var sql in sqlStatements)
            {
                _ctx.Database.ExecuteSqlCommand(sql);
            }

        }
    }
}
