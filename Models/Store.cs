using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Couponat.Models
{
    public class Store
    {
        public Store()
        {
            Coupons = new HashSet<Coupon>();
        }
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Arabic Name")]
        public string NameAr { get; set; }
        public string Logo { get; set; }

        
        public string Tags { get; set; }
        public virtual ICollection<Coupon> Coupons { get; set; }
        public virtual ICollection<Offer> Offers{ get; set; }
    }
}