using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(jqgrid_asp.net_mvc.demo.web.Startup))]
namespace jqgrid_asp.net_mvc.demo.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
