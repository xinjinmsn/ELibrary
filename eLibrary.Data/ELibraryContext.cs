using ELibrary.Data.Entities;
using ELibrary.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Data
{
    public class ELibraryContext : DbContext
    {
        public ELibraryContext()
            : base("ELibraryConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        static ELibraryContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ELibraryContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ELibraryMapping.Configure(modelBuilder);
        }

        public DbSet<ApiUser> ApiUsers { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<TagEntry> TagEntries { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<OrderEntry> OrderEntries { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
