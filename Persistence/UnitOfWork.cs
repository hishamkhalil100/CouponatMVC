using Couponat.Core;
using Couponat.Core.Repositories;
using Couponat.Models;
using Couponat.Persistence.Repositories;

namespace Couponat.Persistence
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Coupons = new CouponRepository(_context);
            CouponHits = new CouponHitsRepository(_context);
            Stores = new StoreRepository(_context);
            Categories = new CategoryRepository(_context);
            Offers = new OfferRepository(_context);
            Banners = new BannerRepository(_context);
        }

        public ICouponRepository Coupons { get; private set; }
        public ICouponHitsRepository CouponHits { get; private set; }
        public IOfferHitsRepository OfferHits { get; private set; }
        public IStoreRepository Stores { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IOfferRepository Offers { get; private set; }
        public IBannerRepository Banners { get; private set; }


        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}