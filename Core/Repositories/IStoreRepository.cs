using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Couponat.Models;

namespace Couponat.Core.Repositories
{
    public interface IStoreRepository : IRepository<Store>
    {
        Store GetAllCouponsInStore(Guid id);
        List<Store> GetTopStores();
    }
}