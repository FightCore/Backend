using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace FightCore.Backend
{
    /// <summary>
    /// The program executed to start the ASP net server.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Runs the actual server and starts the backend.
        /// </summary>
        /// <param name="args">The arguments used to build the configuration.</param>
        public static void Main(string[] args)
        {
            Console.Title = "FightCore Backend";

            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates the web host that will run the 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var configuration = CreateConfiguration(args);

            // UseConfiguration does nothing for me which is cool.
            // Just set it as a static.
            Startup.Configuration = configuration;

            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseConfiguration(configuration)
                .UseSerilog((context, serilogConfiguration) =>
                {
                    serilogConfiguration
                        .MinimumLevel.Information()
                        .Enrich.FromLogContext()
                        .WriteTo.Console(
                            outputTemplate:
                            "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
                            theme: AnsiConsoleTheme.Literate);
                });
        }

        private static IConfiguration CreateConfiguration(IEnumerable<string> args)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            configBuilder.AddEnvironmentVariables();

            configBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            
            foreach (var argument in args)
            {
                configBuilder.AddJsonFile($"appsettings.{argument}.json", false, true);
            }

            return configBuilder.Build();
        }
    }
}
