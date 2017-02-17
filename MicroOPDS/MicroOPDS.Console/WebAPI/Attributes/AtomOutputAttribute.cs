using System;
using System.Web.Http.Controllers;

namespace MicroOPDS.Console
{
    public class AtomOutputAttribute : Attribute, IControllerConfiguration
    {
        public void Initialize(HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor)
        {
            controllerSettings.Formatters.Clear();
            controllerSettings.Formatters.Add(new AtomXmlMediaTypeFormatter());
        }
    }
}
