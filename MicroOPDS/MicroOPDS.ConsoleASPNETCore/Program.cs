using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//http://stackoverflow.com/questions/35997873/migrating-from-owin-to-asp-net-core
//http://dotnetthoughts.net/how-to-host-your-aspnet-core-in-a-windows-service/
//http://benfoster.io/blog/how-to-configure-kestrel-urls-in-aspnet-core-rc2
//http://stackoverflow.com/questions/40128464/running-asp-net-core-mvc-as-a-console-application-project-without-net-core-sdk
//https://blogs.msdn.microsoft.com/webdev/2014/11/14/katana-asp-net-5-and-bridging-the-gap/
//https://taskmatics.com/blog/host-asp-net-in-a-windows-service/
//https://blog.uship.com/shippingcode/self-hosting-a-net-api-choosing-between-owin-with-asp-net-web-api-and-asp-net-core-mvc-1-0/
//https://docs.microsoft.com/en-us/aspnet/core/fundamentals/hosting

namespace MicroOPDS.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                      .UseKestrel()
                      //.UseContentRoot(Directory.GetCurrentDirectory())
                      .UseUrls("http://localhost:9000")
                      .UseIISIntegration()
                      .UseStartup<Startup>()
                      .Build();
            host.Run();
        }
    }

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        private IHostingEnvironment CurrentEnvironment { get; set; }
        private IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddRazorOptions(options =>
            {
                var previous = options.CompilationCallback;
                options.CompilationCallback = context =>
                {
                    previous?.Invoke(context);
                    var refs = AppDomain.CurrentDomain.GetAssemblies()
                           .Where(x => !x.IsDynamic)
                           .Select(x => MetadataReference.CreateFromFile(x.Location))
                           .ToList();
                    context.Compilation = context.Compilation.AddReferences(refs);
                };
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            //app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "default",
                  template: "{controller=opds}/{action=Index}/{id?}");
            });
        }
    }

    [Route("opds")]
    public class opdsController : Controller
    {
        [Route("")]
        [HttpGet]
        public string Get(String data)
        {
            return "Hello";
        }
    }
}
