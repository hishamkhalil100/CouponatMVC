using System;
using System.ComponentModel.DataAnnotations;

namespace Couponat.Models
{
    public class Banner
    {
        public Guid Id { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public bool IsVisible { get; set; }
        public DateTime EndDate { get; set; }

    }
}