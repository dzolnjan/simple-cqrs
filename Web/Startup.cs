using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cqrs.Web.Startup))]
namespace Cqrs.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
