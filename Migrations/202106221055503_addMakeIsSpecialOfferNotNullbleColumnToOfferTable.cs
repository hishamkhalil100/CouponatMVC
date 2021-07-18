namespace Couponat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMakeIsSpecialOfferNotNullbleColumnToOfferTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Offers", "IsSpecialOffer", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Offers", "IsSpecialOffer", c => c.Boolean());
        }
    }
}
