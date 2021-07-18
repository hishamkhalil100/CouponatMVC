using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Couponat.Models
{
    public class Category
    {
        public Category()
        {
            Coupons = new HashSet<Coupon>();
        }
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string NameAr { get; set; }

        public virtual ICollection<Coupon> Coupons{ get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
    }
}