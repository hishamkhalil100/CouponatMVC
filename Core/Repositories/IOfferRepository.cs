using System.Collections.Generic;
using Couponat.Models;

namespace Couponat.Core.Repositories
{
    public interface IOfferRepository : IRepository<Offer>
    {
        //ICollection< Coupon> OrderCouponsByHits();
        void OrderOffersByHits();
        List<Offer> GetOffersWithHits();
        List<Offer> GetOffersWithHits(string searchString);
        List<Offer> GetTopOffers();

    }
}