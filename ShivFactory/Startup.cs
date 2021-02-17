using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShivFactory.Startup))]
namespace ShivFactory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
