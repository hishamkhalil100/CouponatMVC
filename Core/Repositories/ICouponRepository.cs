using System.Collections.Generic;
using System.Linq;
using Couponat.Models;

namespace Couponat.Core.Repositories
{
    public interface ICouponRepository :IRepository<Coupon>
    {
        //ICollection< Coupon> OrderCouponsByHits();
        void OrderCouponsByHits();
        List<Coupon> GetCouponsWithHits();
        List<Coupon> GetCouponsWithHits(string searchString);
        List<Coupon> GetTopCoupons();

    }
}