namespace Couponat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setCouponCreationDateAsNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Coupons", "CreationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Coupons", "CreationDate", c => c.DateTime(nullable: false));
        }
    }
}
