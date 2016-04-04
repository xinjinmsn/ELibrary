namespace ELibrary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Book.Book", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Book.Book", "Image");
        }
    }
}
