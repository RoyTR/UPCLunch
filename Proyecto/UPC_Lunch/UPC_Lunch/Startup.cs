using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UPC_Lunch.Startup))]
namespace UPC_Lunch
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
