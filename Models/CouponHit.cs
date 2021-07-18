using System;

namespace Couponat.Models
{
    public partial class CouponHit
    {
        public Guid Id { get; set; }

        public Guid CouponId { get; set; }

        public Guid DeviceId { get; set; }

        public HitTypes HitType { get; set; }

        public DateTime? CreationDate { get; set; }

        public virtual Coupon Coupon { get; set; }

        public virtual Device Device { get; set; }
    }

   
}
