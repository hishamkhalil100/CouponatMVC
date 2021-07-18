using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Couponat.Models;

namespace Couponat.Persistence.EntityConfigrations
{
    public class StoreConfiguration:EntityTypeConfiguration<Store>
    
    {
        public StoreConfiguration()
        {
            HasKey(c => c.Id);
            HasMany<Coupon>(g => g.Coupons)
                .WithOptional(s => s.Store);

            HasMany<Offer>(g => g.Offers)
                .WithOptional(s => s.Store);
        }
    }
}