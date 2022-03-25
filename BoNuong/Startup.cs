using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BoNuong.Startup))]
namespace BoNuong
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
