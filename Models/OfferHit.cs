using System;

namespace Couponat.Models
{
    public partial class OfferHit
    {
        public Guid Id { get; set; }

        public Guid OfferId { get; set; }

        public Guid DeviceId { get; set; }

        public HitTypes HitType { get; set; }

        public DateTime? CreationDate { get; set; }

        public virtual Device Device { get; set; }

        public virtual Offer Offer { get; set; }
    }
}
