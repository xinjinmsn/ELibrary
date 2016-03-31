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

        public IQueryable<Tag> GetAllTags()
        {
            return _ctx.Tags;
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

        public Order GetOrder(string userName, DateTime orderId)
        {
            return GetOrders(userName).Where(d => d.CurrentDate == orderId.Date).FirstOrDefault();
        }

        public IQueryable<OrderEntry> GetOrderEntries(string userName, DateTime orderId)
        {
            return _ctx.OrderEntries.Include("BookItem")
                                    .Include("Order")
                                    .Where(f=>f.Order.UserName==userName && f.Order.CurrentDate==orderId);
        }

        public OrderEntry GetOrderEntry(string userName, DateTime orderId, int id)
        {
            return _ctx.OrderEntries.Include("BookItem")
                                    .Include("Order")
                                    .Where(f => f.Order.UserName == userName && 
                                           f.Order.CurrentDate == orderId && 
                                           f.Id == id)
                                    .FirstOrDefault();
        }

        public bool DeleteOrderEntry(int id)
        {
            try
            {
                var entity = _ctx.OrderEntries.Where(f=>f.Id==id).FirstOrDefault();

                if(entity!=null)
                {
                    _ctx.OrderEntries.Remove(entity);
                    return true;
                }
               
            }
            catch
            {
                //TODO logging
            }

            return false;
        }

        public void ClearDB()
        {
            _ctx.Tags.RemoveRange(_ctx.Tags.ToList());
            _ctx.Books.RemoveRange(_ctx.Books.ToList());
            _ctx.Orders.RemoveRange(_ctx.Orders.ToList());
            _ctx.OrderEntries.RemoveRange(_ctx.OrderEntries.ToList());
            _ctx.Authors.RemoveRange(_ctx.Authors.ToList());

            _ctx.SaveChanges();
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0; 
        }
    }
}
