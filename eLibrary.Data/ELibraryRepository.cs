using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELibrary.Data.Entities;
using System.Data.Entity;

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

        public IQueryable<Tag> GetTagsForBook(int bookid)
        {
            return _ctx.Tags.Include("Books")
                .Where(f => f.Books.Any(m=>m.Id==bookid));
        }

        public Book GetBook(int id)
        {
            return _ctx.Books.Include("Author").Include("Tags").Where(f => f.Id == id).FirstOrDefault();
        }

        public Tag GetTag(int id)
        {
            return _ctx.Tags.Include(m=>m.Books.Select(i=>i.Author)).Where(f => f.Id == id).FirstOrDefault();
            
        }

        public Tag GetTag(int bookid, int id)
        {
            return GetTagsForBook(bookid).ToList().Where(f=>f.Id==id).FirstOrDefault();
        }


        public IQueryable<Order> GetOrders(string userName)
        {
            return _ctx.Orders.Include("Entries.BookItem")
                .OrderByDescending(d=>d.CurrentDate)
                .Where(d=>d.UserName == userName);
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0; 
        }
    }
}
