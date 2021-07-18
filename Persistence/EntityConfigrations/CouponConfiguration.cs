using System.Data.Entity.ModelConfiguration;
using Couponat.Models;

namespace Couponat.Persistence.EntityConfigrations
{
    public class CouponConfiguration : EntityTypeConfiguration<Coupon>
    {
        public CouponConfiguration()
        {
            //ToTable("Coupons");
            HasKey(c => c.Id);
            HasMany(e => e.CouponHits)
                .WithRequired(e => e.Coupon)
                .WillCascadeOnDelete(false);
        }

    }
}