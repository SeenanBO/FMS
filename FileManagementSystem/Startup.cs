using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FileManagementSystem.Startup))]
namespace FileManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
