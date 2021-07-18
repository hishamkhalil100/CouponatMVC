using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Couponat.Models
{
    public partial class Coupon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Coupon()
        {
            CouponHits = new HashSet<CouponHit>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string NameAr { get; set; }

        [Required]
        [StringLength(200)]
        public string Offer { get; set; }

        [Required]
        [StringLength(200)]
        public string OfferAr { get; set; }

        [Required]
        [StringLength(50)]
        public string CouponCode { get; set; }

        [StringLength(255)]
        public string Logo { get; set; }

        [StringLength(255)]
        public string Website { get; set; }

        public string CouponDetails { get; set; }

        public string CouponDetailsAr { get; set; }

        [StringLength(255)]
        public string Tags { get; set; }

        public DateTime? EndDate { get; set; }
       
        public DateTime? CreationDate {
            get;
           set;
        }
        public int? CategoryId { get; set; }

        public Category Category { get; set; }
        public Guid? StoreId { get; set; }
        public Store Store { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CouponHit> CouponHits { get; set; }
    }
}
