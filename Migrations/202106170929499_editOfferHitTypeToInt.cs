namespace Couponat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editOfferHitTypeToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OfferHits", "HitType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OfferHits", "HitType", c => c.Int());
        }
    }
}
