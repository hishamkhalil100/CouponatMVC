using System.Data.Entity.ModelConfiguration;
using Couponat.Models;

namespace Couponat.Persistence.EntityConfigrations
{
    public class OfferConfigration : EntityTypeConfiguration<Offer>
    {
        public OfferConfigration()
        {
            
                HasMany(e => e.OfferHits)
                .WithRequired(e => e.Offer)
                .WillCascadeOnDelete(false);
        }
    }
}