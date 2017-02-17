using System;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;

namespace MicroOPDS.Console
{
    public class JsonOutputAttribute : Attribute, IControllerConfiguration
    {
        public void Initialize(HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor)
        {
            controllerSettings.Formatters.Clear();
            controllerSettings.Formatters.Add(new JsonMediaTypeFormatter());
        }
    }
}
