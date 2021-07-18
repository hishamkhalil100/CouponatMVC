using System;
using Couponat.Core.Repositories;

namespace Couponat.Core
{
    public interface IUnitOfWork : IDisposable
    {
         ICouponRepository Coupons { get; }
         ICouponHitsRepository CouponHits { get;}
         IStoreRepository Stores { get; }
         ICategoryRepository Categories { get;  }
         IOfferRepository Offers{ get; }
         IOfferHitsRepository OfferHits { get; }
         IBannerRepository Banners { get; }
         int Complete();
    }
}