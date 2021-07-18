using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Couponat.Startup))]
namespace Couponat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
