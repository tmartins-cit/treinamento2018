using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(Treinamento.Presentation.WebApi.Startup))]

namespace Treinamento.Presentation.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string completePath = System.IO.Directory.GetParent(path).Parent.FullName;
            AppDomain.CurrentDomain.SetData("DataDirectory", completePath);
            
            ConfigureAuth(app);
        }
    }
}
