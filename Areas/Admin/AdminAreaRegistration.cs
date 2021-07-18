using System.Web.Mvc;

namespace Couponat.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Coupons", action = "Index", id = UrlParameter.Optional },
                new[] { "Couponat.Areas.Admin.Controllers" }
            );
        }
    }
}