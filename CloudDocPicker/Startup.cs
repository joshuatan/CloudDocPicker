using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CloudDocPicker.Startup))]
namespace CloudDocPicker
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
