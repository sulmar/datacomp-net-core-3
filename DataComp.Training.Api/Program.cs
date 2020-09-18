using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;

namespace DataComp.Training.Api
{

   

    public class Program
    {
        public static void Main(string[] args)
        {
            // dotnet add package Serilog.AspNetCore

            // dotnet add package Serilog.Enrichers.Thread

            Log.Logger = new LoggerConfiguration()
                .Enrich.WithThreadId()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
                .WriteTo.File(new CompactJsonFormatter(), "logs/log.json")
                .CreateLogger();

            // https://github.com/serilog/serilog-sinks-mssqlserver

                CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
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
                })
                .UseSerilog();
    }
}
