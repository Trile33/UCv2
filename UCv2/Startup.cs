using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UCv2.Startup))]
namespace UCv2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
