using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lab5.Startup))]
namespace lab5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
