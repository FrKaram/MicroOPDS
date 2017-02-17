using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroOPDS.Console
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "opds",
                routeTemplate: "{*data}",
                defaults: new { controller = "opds", uri = RouteParameter.Optional });

            //From dotOPDS
            //config.Routes.MapHttpRoute(
            //    "Web",
            //    "{*filename}",
            //    new { controller = "Web", action = "ServeIndex" }
            //);

            //if (Settings.Instance.Authentication.Enabled) appBuilder.UseBasicAuthentication();
            appBuilder.UseWebApi(config);
        }
    }
}
