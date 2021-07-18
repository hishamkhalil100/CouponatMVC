using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Couponat.Models
{
    public partial class Offer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Offer()
        {
            OfferHits = new HashSet<OfferHit>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string NameAr { get; set; }

        [Required]
        [StringLength(50)]
        public string CouponCode { get; set; }

        [StringLength(255)]
        public string Image { get; set; }

        [StringLength(255)]
        public string Website { get; set; }

        public string OfferDetails { get; set; }

        public string OfferDetailsAr { get; set; }

        public bool IsSpecialOffer { get; set; }

        [StringLength(255)]
        public string Tags { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }

        public DateTime CreationDate { get; set; }
        public int? CategoryId { get; set; }

        public Category Category { get; set; }
        public Guid? StoreId { get; set; }
        public Store Store { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OfferHit> OfferHits { get; set; }
    }
}
