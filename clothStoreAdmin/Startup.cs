using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(clothStoreAdmin.Startup))]
namespace clothStoreAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
