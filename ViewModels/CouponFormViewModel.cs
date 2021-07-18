using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Couponat.Models;

namespace Couponat.ViewModels
{
    public class CouponFormViewModel
    {
        public Coupon Coupon { get; set; }
        public IEnumerable<Store> Stores{ get; set; }
        public IEnumerable<Category> Categories  { get; set; }
    }
}