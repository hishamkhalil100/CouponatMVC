namespace Couponat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMakeImageNullbleColumnToBannerTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Banners", "Image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Banners", "Image", c => c.String(nullable: false));
        }
    }
}
