using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Couponat.Models
{
    public partial class Device
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Device()
        {
            CouponHits = new HashSet<CouponHit>();
            OfferHits = new HashSet<OfferHit>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string DeviceId { get; set; }

        public int DeviceType { get; set; }

        public DateTime? CreationDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CouponHit> CouponHits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OfferHit> OfferHits { get; set; }
    }
}
