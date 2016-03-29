using ELibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Data
{
    public interface IELibraryRepository
    {
        //General
        bool SaveAll();

        // Book
        IQueryable<Book> GetAllBooks();
        IQueryable<Book> GetAllBooksWithTags();

        IQueryable<Tag> GetTagsForBook(int bookid);

        Book GetBook(int id);

        //Tag
        Tag GetTag(int id);
        Tag GetTag(int bookid, int id);
        IQueryable<Tag> GetAllTags();

        //Order
        IQueryable<Order> GetOrders(string userName);
        Order GetOrder(string userName, DateTime orderId);

        IQueryable<OrderEntry> GetOrderEntries(string userName, DateTime orderId);
        OrderEntry GetOrderEntry(string userName, DateTime orderId, int id);

        bool DeleteOrderEntry(int id);
    }
}
