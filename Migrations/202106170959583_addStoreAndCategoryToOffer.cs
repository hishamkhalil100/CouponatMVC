namespace Couponat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStoreAndCategoryToOffer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Offers", "CategoryId", c => c.Int());
            AddColumn("dbo.Offers", "StoreId", c => c.Guid());
            CreateIndex("dbo.Offers", "CategoryId");
            CreateIndex("dbo.Offers", "StoreId");
            AddForeignKey("dbo.Offers", "StoreId", "dbo.Stores", "Id");
            AddForeignKey("dbo.Offers", "CategoryId", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Offers", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Offers", "StoreId", "dbo.Stores");
            DropIndex("dbo.Offers", new[] { "StoreId" });
            DropIndex("dbo.Offers", new[] { "CategoryId" });
            DropColumn("dbo.Offers", "StoreId");
            DropColumn("dbo.Offers", "CategoryId");
        }
    }
}
