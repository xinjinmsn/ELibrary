using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELibrary.Data.Entities;

namespace ELibrary.Data
{
    public class ELibraryRepository : IELibraryRepository
    {
        private ELibraryContext _ctx;

        public ELibraryRepository(ELibraryContext ctx)
        {
            _ctx = ctx;
        }
        public IQueryable<Book> GetAllBooks()
        {
            return _ctx.Books.Include("Author");
        }

        public IQueryable<Book> GetAllBooksWithTags()
        {
            return _ctx.Books.Include("Author").Include("Tags");
        }

        public Book GetBook(int id)
        {
            return _ctx.Books.Include("Tags").Where(f => f.Id == id).FirstOrDefault();
        }

        public Tag GetTag(int id)
        {
            return _ctx.Tags.Where(f => f.Id == id).FirstOrDefault();
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0; 
        }
    }
}
