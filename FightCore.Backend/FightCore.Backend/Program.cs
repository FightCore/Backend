using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

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
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var configuration = CreateConfiguration(args);

            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseConfiguration(configuration)
                .UseSerilog((context, serilogConfiguration) =>
                {
                    serilogConfiguration
                        .ReadFrom.Configuration(configuration);
                });
        }

        private static IConfiguration CreateConfiguration(IEnumerable<string> args)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            configBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            configBuilder.AddJsonFile("appsettings-local.json", optional: true, reloadOnChange: true);

            configBuilder.AddEnvironmentVariables();
            return configBuilder.Build();
        }
    }
}
