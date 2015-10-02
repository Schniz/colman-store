using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StoreForColman.Startup))]
namespace StoreForColman
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
