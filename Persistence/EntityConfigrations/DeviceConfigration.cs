using System.Data.Entity.ModelConfiguration;
using Couponat.Models;

namespace Couponat.Persistence.EntityConfigrations
{
    public class DeviceConfigration : EntityTypeConfiguration<Device>
    {
        public DeviceConfigration()
        {
            HasKey(e => e.Id);

            HasMany(e => e.CouponHits)
                .WithRequired(e => e.Device)
                .WillCascadeOnDelete(false);


            HasMany(e => e.OfferHits)
                .WithRequired(e => e.Device)
                .WillCascadeOnDelete(false);
        }
      
    }
}