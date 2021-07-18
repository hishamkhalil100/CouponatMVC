using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Couponat.Core.Repositories;
using Couponat.Models;

namespace Couponat.Persistence.Repositories
{
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        public OfferRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void OrderOffersByHits()
        {
            var q = CouponatContext.Offers.OrderByDescending(c => c.OfferHits.Count())
                .Select(c => new Coupon());

            var i = q.ToList();

        }
        public List<Offer> GetTopOffers()
        {
            return CouponatContext.Offers.OrderByDescending(o => o.CreationDate).Take(6).ToList();
        } 
        public List<Offer> GetOffersWithHits()
        {
            return CouponatContext.Offers.Include(c => c.OfferHits).ToList();



        }

        public List<Offer> GetOffersWithHits(string searchString)
        {
            return CouponatContext.Offers.Include(c => c.OfferHits).Where(c => c.Name.Contains(searchString) ||
                                                                               c.NameAr.Contains(searchString)).ToList();
        }

        public ApplicationDbContext CouponatContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}