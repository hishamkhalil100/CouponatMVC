using System;
using System.Collections.Generic;
using System.Linq;
using Couponat.Core.Repositories;
using System.Data.Entity;
using Couponat.Models;

namespace Couponat.Persistence.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Category GetAllCouponsInCategory(int id)
        {
            
            return CouponatContext.Categories.Include(c => c.Coupons).SingleOrDefault(c => c.Id == id);

        }

        public ApplicationDbContext CouponatContext
        {
            get { return Context as ApplicationDbContext; }
        }

        
    }
}