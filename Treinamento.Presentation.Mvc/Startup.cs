using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Treinamento.Presentation.Mvc.Startup))]
namespace Treinamento.Presentation.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
