using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FPTEduSystem.Startup))]
namespace FPTEduSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
