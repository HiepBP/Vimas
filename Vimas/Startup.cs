using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vimas.Startup))]
namespace Vimas
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
