using System;
using System.Collections.Generic;
using System.Dynamic;
using Couponat.Models;

namespace Couponat.Core.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
       Category GetAllCouponsInCategory(int id);
    }
}