using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApp_Futbol.Startup))]
namespace WebApp_Futbol
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
