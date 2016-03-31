namespace ELibrary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Book.Book", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Book.Book", "Description");
        }
    }
}
