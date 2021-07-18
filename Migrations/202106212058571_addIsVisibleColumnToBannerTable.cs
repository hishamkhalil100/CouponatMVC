namespace Couponat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsVisibleColumnToBannerTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Banners", "IsVisible", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Banners", "IsVisible");
        }
    }
}
