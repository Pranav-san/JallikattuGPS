using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Jallikattu.Startup))]

namespace Jallikattu
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndAdmin();
            CreateTestUser();
        }
    }
}
