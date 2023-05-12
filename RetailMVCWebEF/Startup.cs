
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RetailMVCWebEF.Startup))]
namespace RetailMVCWebEF
{
    public partial class Startup
    {
        public virtual void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}