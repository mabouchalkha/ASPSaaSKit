using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StarterKit.Startup))]
namespace StarterKit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
