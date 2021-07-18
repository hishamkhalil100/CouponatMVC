namespace Couponat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addBannerTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banners",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Image = c.String(nullable: false),
                        Url = c.String(),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Banners");
        }
    }
}
