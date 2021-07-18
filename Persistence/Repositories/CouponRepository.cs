using System.Collections.Generic;
using System.Linq;
using Couponat.Core.Repositories;
using System.Data.Entity;
using Couponat.Models;

namespace Couponat.Persistence.Repositories
{
    public class CouponRepository : Repository<Coupon>, ICouponRepository
    {
        public CouponRepository(ApplicationDbContext context) : base(context)
        {
        }
        
        public void OrderCouponsByHits()
        {
            var q= CouponatContext.Coupons.OrderByDescending(c => c.CouponHits.Count())
                .Select(c => new Coupon());

            var i = q.ToList();

        }
        public List<Coupon> GetTopCoupons()
        {
            return CouponatContext.Coupons.OrderByDescending(c => c.CreationDate).Take(10).ToList();

        }
        public List<Coupon> GetCouponsWithHits()
        {
            return CouponatContext.Coupons.Include(c => c.CouponHits).ToList();



        }

        public List<Coupon> GetCouponsWithHits(string searchString)
        {
            return CouponatContext.Coupons.Include(c => c.CouponHits).Where(c => c.Name.Contains(searchString) ||
                                                                                 c.NameAr.Contains(searchString)).ToList();
        }

        public ApplicationDbContext CouponatContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}