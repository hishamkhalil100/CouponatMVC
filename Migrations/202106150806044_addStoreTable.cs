namespace Couponat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStoreTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        NameAr = c.String(nullable: false, maxLength: 50),
                        Logo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Coupons", "StoreId", c => c.Guid());
            CreateIndex("dbo.Coupons", "StoreId");
            AddForeignKey("dbo.Coupons", "StoreId", "dbo.Stores", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Coupons", "StoreId", "dbo.Stores");
            DropIndex("dbo.Coupons", new[] { "StoreId" });
            DropColumn("dbo.Coupons", "StoreId");
            DropTable("dbo.Stores");
        }
    }
}
