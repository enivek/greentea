using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace GreenTea.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            Log.Information("Application Starting Up");
            RunApplication();
        }

        private static void RunApplication()
        {
            try
            {
                var environment = _getEnvironment();
                Log.Information("Environment: " + environment);

                var configuration = _getConfiguration();

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom
                    .Configuration(configuration)
                    .CreateLogger()
                    .ForContext("Machine", Environment.MachineName)
                    .ForContext("Application", configuration["AppSettings:Application"])
                    .ForContext("Environment", environment);

                Log.Information("Starting application: " + configuration["AppSettings:Application"]);

                var host = _buildWebHost(configuration, environment);
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IWebHost _buildWebHost(IConfiguration configuration, string environment)
        {
            return WebHost.CreateDefaultBuilder()
                .UseEnvironment(environment)
                .UseConfiguration(configuration)
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
        }

        private static IConfiguration _getConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var currentDirectory = Directory.GetCurrentDirectory();
            configurationBuilder.SetBasePath(currentDirectory)
              .AddJsonFile("appsettings.json", false, true)
              .AddEnvironmentVariables();
            return configurationBuilder.Build();
        }

        private static string _getEnvironment()
        {
            return Environment.GetEnvironmentVariable("ENVIRONMENT");
        }

    }
}
