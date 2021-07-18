namespace Couponat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCategoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        NameAr = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Coupons", "CategoryId", c => c.Int());
            CreateIndex("dbo.Coupons", "CategoryId");
            AddForeignKey("dbo.Coupons", "CategoryId", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Coupons", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Coupons", new[] { "CategoryId" });
            DropColumn("dbo.Coupons", "CategoryId");
            DropTable("dbo.Categories");
        }
    }
}
