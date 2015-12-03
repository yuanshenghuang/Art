using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("Kunst", typeof(Kunst.Startup))]
namespace Kunst
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
