using System.ComponentModel.DataAnnotations;

namespace Couponat.ViewModels.Identity
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}