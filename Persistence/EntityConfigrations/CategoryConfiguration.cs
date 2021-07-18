using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Couponat.Models;

namespace Couponat.Persistence.EntityConfigrations
{
    public class CategoryConfiguration :EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
                 HasKey(c => c.Id);
                 HasMany<Coupon>(g => g.Coupons)
                     .WithOptional(s => s.Category);

            HasMany<Offer>(g => g.Offers)
                .WithOptional(s => s.Category);
        }
    }
}