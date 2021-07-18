
using System;
using System.Collections.Generic;
using System.Linq;
using Couponat.Core.Repositories;
using System.Data.Entity;
using Couponat.Models;

namespace Couponat.Persistence.Repositories
{
    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        public StoreRepository(ApplicationDbContext context) : base(context)
        {
        }
        
        public Store GetAllCouponsInStore(Guid id)
        {

            return CouponatContext.Stores.Include(c => c.Coupons).SingleOrDefault(c => c.Id == id);

        }

        public List<Store> GetTopStores()
        {
            return CouponatContext.Stores.OrderByDescending(s => s.Id).Take(10).ToList();

        }

        public ApplicationDbContext CouponatContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}