using System.Collections.Generic;
using Couponat.Models;

namespace Couponat.ViewModels
{
    public class OfferFormViewModel 
    {
        public Offer Offer { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Store> Stores { get; set; }
    }
}