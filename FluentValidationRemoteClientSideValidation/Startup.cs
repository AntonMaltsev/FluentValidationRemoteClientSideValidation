using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FluentValidationRemoteClientSideValidation.Startup))]
namespace FluentValidationRemoteClientSideValidation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
