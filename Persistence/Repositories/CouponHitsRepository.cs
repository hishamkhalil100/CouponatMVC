using Couponat.Core.Repositories;
using Couponat.Models;

namespace Couponat.Persistence.Repositories
{
    public class CouponHitsRepository : Repository<CouponHit>, ICouponHitsRepository
    {
        public CouponHitsRepository(ApplicationDbContext context) : base(context)
        {
        }



        public ApplicationDbContext CouponatContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}