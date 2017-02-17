using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOPDS.Console
{
    class Program
    {
        private IDisposable server;

        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";

            StartOptions options = new StartOptions();
            options.Urls.Add(baseAddress);
            //options.Urls.Add("http://127.0.0.1:9095");
            //options.Urls.Add(string.Format("http://{0}:9095", Environment.MachineName));

            var WebServer = WebApp.Start<Startup>(options);

            System.Console.ReadLine();
            WebServer.Dispose();
            System.Console.ReadLine();
        }
    }
}
