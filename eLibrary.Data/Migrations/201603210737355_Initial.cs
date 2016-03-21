namespace ELibrary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Security.ApiUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Secret = c.String(),
                        AppId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Book.Author",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Security.AuthToken",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Token = c.String(),
                        Expiration = c.DateTime(nullable: false),
                        ApiUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Security.ApiUser", t => t.ApiUser_Id)
                .Index(t => t.ApiUser_Id);
            
            CreateTable(
                "Book.Book",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Year = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Pages = c.Int(nullable: false),
                        Stock = c.Int(nullable: false),
                        Author_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Book.Author", t => t.Author_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "Book.Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Library.OrderEntry",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        BookItem_Id = c.Int(),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Book.Book", t => t.BookItem_Id)
                .ForeignKey("Library.Order", t => t.Order_Id)
                .Index(t => t.BookItem_Id)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "Library.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CurrentDate = c.DateTime(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagBooks",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Book_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Book_Id })
                .ForeignKey("Book.Tag", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("Book.Book", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Book_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Library.OrderEntry", "Order_Id", "Library.Order");
            DropForeignKey("Library.OrderEntry", "BookItem_Id", "Book.Book");
            DropForeignKey("dbo.TagBooks", "Book_Id", "Book.Book");
            DropForeignKey("dbo.TagBooks", "Tag_Id", "Book.Tag");
            DropForeignKey("Book.Book", "Author_Id", "Book.Author");
            DropForeignKey("Security.AuthToken", "ApiUser_Id", "Security.ApiUser");
            DropIndex("dbo.TagBooks", new[] { "Book_Id" });
            DropIndex("dbo.TagBooks", new[] { "Tag_Id" });
            DropIndex("Library.OrderEntry", new[] { "Order_Id" });
            DropIndex("Library.OrderEntry", new[] { "BookItem_Id" });
            DropIndex("Book.Book", new[] { "Author_Id" });
            DropIndex("Security.AuthToken", new[] { "ApiUser_Id" });
            DropTable("dbo.TagBooks");
            DropTable("Library.Order");
            DropTable("Library.OrderEntry");
            DropTable("Book.Tag");
            DropTable("Book.Book");
            DropTable("Security.AuthToken");
            DropTable("Book.Author");
            DropTable("Security.ApiUser");
        }
    }
}
