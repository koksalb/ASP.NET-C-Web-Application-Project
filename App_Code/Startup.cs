using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(project1.Startup))]
namespace project1
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
