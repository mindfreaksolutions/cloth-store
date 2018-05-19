using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(clothStore.Startup))]
namespace clothStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
