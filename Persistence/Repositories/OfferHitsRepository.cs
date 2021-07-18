using Couponat.Core.Repositories;
using Couponat.Models;

namespace Couponat.Persistence.Repositories
{
    public class OfferHitsRepository : Repository<OfferHit>, IOfferHitsRepository
    {
        public OfferHitsRepository(ApplicationDbContext context) : base(context)
        {
        }



        public ApplicationDbContext CouponatContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }

   
}