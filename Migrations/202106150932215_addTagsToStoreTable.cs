namespace Couponat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTagsToStoreTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stores", "Tags", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stores", "Tags");
        }
    }
}
