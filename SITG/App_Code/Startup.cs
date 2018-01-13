using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sitg.Startup))]
namespace sitg
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
