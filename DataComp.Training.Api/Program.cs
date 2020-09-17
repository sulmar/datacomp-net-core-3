using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DataComp.Training.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config)=>
                {
                    string environmentName = hostingContext.HostingEnvironment.EnvironmentName;

                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddXmlFile("appsettings.xml", optional: true, reloadOnChange: true);
                    // config.AddDb("connectionstring");

                    config.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
                    config.AddUserSecrets(Assembly.GetExecutingAssembly());
                
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
