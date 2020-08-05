using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TEST.App_Start.Startup))]
namespace TEST.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}