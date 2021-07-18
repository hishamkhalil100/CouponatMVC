namespace Couponat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addInitCouponatTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CouponHits",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CouponId = c.Guid(nullable: false),
                        DeviceId = c.Guid(nullable: false),
                        HitType = c.Int(nullable: false),
                        CreationDate = c.DateTime(defaultValue:DateTime.Now),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coupons", t => t.CouponId)
                .ForeignKey("dbo.Devices", t => t.DeviceId)
                .Index(t => t.CouponId)
                .Index(t => t.DeviceId);
            
            CreateTable(
                "dbo.Coupons",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        NameAr = c.String(nullable: false, maxLength: 50),
                        Offer = c.String(nullable: false, maxLength: 200),
                        OfferAr = c.String(nullable: false, maxLength: 200),
                        CouponCode = c.String(nullable: false, maxLength: 50),
                        Logo = c.String(maxLength: 255),
                        Website = c.String(maxLength: 255),
                        CouponDetails = c.String(),
                        CouponDetailsAr = c.String(),
                        Tags = c.String(maxLength: 255),
                        EndDate = c.DateTime(),
                        CreationDate = c.DateTime(defaultValue: DateTime.Now),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceId = c.String(nullable: false, maxLength: 255),
                        DeviceType = c.Int(nullable: false),
                        CreationDate = c.DateTime(defaultValue: DateTime.Now),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OfferHits",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OfferId = c.Guid(nullable: false),
                        DeviceId = c.Guid(nullable: false),
                        HitType = c.Int(),
                        CreationDate = c.DateTime(defaultValue: DateTime.Now),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Offers", t => t.OfferId)
                .ForeignKey("dbo.Devices", t => t.DeviceId)
                .Index(t => t.OfferId)
                .Index(t => t.DeviceId);
            
            CreateTable(
                "dbo.Offers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        NameAr = c.String(nullable: false, maxLength: 50),
                        CouponCode = c.String(nullable: false, maxLength: 50),
                        Image = c.String(maxLength: 255),
                        Website = c.String(maxLength: 255),
                        OfferDetails = c.String(),
                        OfferDetailsAr = c.String(),
                        IsSpecialOffer = c.Boolean(),
                        Tags = c.String(maxLength: 255),
                        EndDate = c.DateTime(),
                        CreationDate = c.DateTime(defaultValue: DateTime.Now),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OfferHits", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.OfferHits", "OfferId", "dbo.Offers");
            DropForeignKey("dbo.CouponHits", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.CouponHits", "CouponId", "dbo.Coupons");
            DropIndex("dbo.OfferHits", new[] { "DeviceId" });
            DropIndex("dbo.OfferHits", new[] { "OfferId" });
            DropIndex("dbo.CouponHits", new[] { "DeviceId" });
            DropIndex("dbo.CouponHits", new[] { "CouponId" });
            DropTable("dbo.Offers");
            DropTable("dbo.OfferHits");
            DropTable("dbo.Devices");
            DropTable("dbo.Coupons");
            DropTable("dbo.CouponHits");
        }
    }
}
