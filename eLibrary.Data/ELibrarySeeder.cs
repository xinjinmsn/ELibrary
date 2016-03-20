//#define TEST_SEED
//#define FORCE_RECREATE

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
        "DELETE FROM Book.TagEntry",
        "DELETE FROM Book.Tag",
        "DELETE FROM Book.Book",
        "DELETE FROM Book.Author"
      );
#endif


            SeedTags();
            SeedBooks();


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

        void SeedBooks()
        {


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

            book.Tags.Add(new TagEntry
            {
                BookItem = book,
                TagItem = _ctx.Tags.Where(f => f.Name == "Programming").FirstOrDefault()
            });

            book.Tags.Add(new TagEntry
            {
                BookItem = book,
                TagItem = _ctx.Tags.Where(f => f.Name == "C#").FirstOrDefault()
            });

            book.Tags.Add(new TagEntry
            {
                BookItem = book,
                TagItem = _ctx.Tags.Where(f => f.Name == ".Net").FirstOrDefault()
            });

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

            book.Tags.Add(new TagEntry
            {
                BookItem = book,
                TagItem = _ctx.Tags.Where(f => f.Name == "Software Design & Engineering").FirstOrDefault()
            });

            book.Tags.Add(new TagEntry
            {
                BookItem = book,
                TagItem = _ctx.Tags.Where(f => f.Name == "Enterprise Applications").FirstOrDefault()
            });

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

            book.Tags.Add(new TagEntry
            {
                BookItem = book,
                TagItem = _ctx.Tags.Where(f => f.Name == "Programming").FirstOrDefault()
            });

            book.Tags.Add(new TagEntry
            {
                BookItem = book,
                TagItem = _ctx.Tags.Where(f => f.Name == "Web Design").FirstOrDefault()
            });

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

            book.Tags.Add(new TagEntry
            {
                BookItem = book,
                TagItem = _ctx.Tags.Where(f => f.Name == "Programming").FirstOrDefault()
            });

            book.Tags.Add(new TagEntry
            {
                BookItem = book,
                TagItem = _ctx.Tags.Where(f => f.Name == "Web Design").FirstOrDefault()
            });

            book.Tags.Add(new TagEntry
            {
                BookItem = book,
                TagItem = _ctx.Tags.Where(f => f.Name == "JavaScript").FirstOrDefault()
            });

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

            book.Tags.Add(new TagEntry
            {
                BookItem = book,
                TagItem = _ctx.Tags.Where(f => f.Name == "Software Design & Engineering").FirstOrDefault()
            });


            _ctx.Books.Add(book);


            //Save all changes
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
