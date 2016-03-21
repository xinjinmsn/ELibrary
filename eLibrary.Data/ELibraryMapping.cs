using ELibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Data
{
    public class ELibraryMapping
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            MapAuthor(modelBuilder);
            MapTag(modelBuilder);
            MapBook(modelBuilder);
            MapOrderEntry(modelBuilder);
            MapOrder(modelBuilder);
            MapApiToken(modelBuilder);
            MapApiUser(modelBuilder);
        }


        static void MapAuthor(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().ToTable("Author", "Book");
        }

        static void MapTag(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>().ToTable("Tag", "Book");
        }
        static void MapBook(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Book", "Book");
        }

        static void MapOrderEntry(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderEntry>().ToTable("OrderEntry", "Library");
        }

        static void MapOrder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Order", "Library");
        }
        static void MapApiToken(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthToken>().ToTable("AuthToken", "Security");
        }

        static void MapApiUser(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApiUser>().ToTable("ApiUser", "Security");
        }
    }

}
