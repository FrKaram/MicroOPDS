using System;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;

namespace MicroOPDS.Console
{
    public class XMLOutputAttribute : Attribute, IControllerConfiguration
    {
        public void Initialize(HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor)
        {
            controllerSettings.Formatters.Clear();
            controllerSettings.Formatters.Add(new XmlMediaTypeFormatter());
        }
    }
}
