using System.Data.Entity;
using Couponat.Persistence.EntityConfigrations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Couponat.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<CouponHit> CouponHits { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<OfferHit> OfferHits { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new CouponConfiguration());
            modelBuilder.Configurations.Add(new DeviceConfigration());
            modelBuilder.Configurations.Add(new OfferConfigration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new StoreConfiguration());
            modelBuilder.Configurations.Add(new BannerConfiguration());

        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}