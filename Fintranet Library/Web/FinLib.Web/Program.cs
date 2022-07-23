using FinLib.Models.Configs;
using FinLib.Models.Constants.Database;
using FinLib.Providers.Logging;
using FinLib.Web.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System.Diagnostics;
using System.Threading.Tasks;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace FinLib.Web
{
    public static class Program
    {
        private static readonly AppLogger _logger = new();

        public static async Task Main(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args).Build();

                initLogging(host);

                logStartingApplicationInfo(host);

                registerHostApplicationEvents(host);

                await host.RunAsync();
            }
            catch (System.Exception ex)
            {
                // catch setup errors
                _logger.Fatal(FinLib.Models.Enums.EventCategory.Application
                    , FinLib.Models.Enums.EventId.Processing
                    , FinLib.Models.Enums.EventType.Failure
                                , "Fatal error in application"
                                , ex);

                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        private static void registerHostApplicationEvents(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var applicationLifeTimeHandler = scope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();

                applicationLifeTimeHandler.ApplicationStarted.Register(onApplicationStarted);
                applicationLifeTimeHandler.ApplicationStopping.Register(onApplicationStopping);
                applicationLifeTimeHandler.ApplicationStopped.Register(onApplicationStopped);
            }
        }

        private static void initLogging(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                // fetch config from appsettings.json
                var theGlobalSettings = new GlobalSettings();
                configuration.GetSection("GlobalSettings").Bind(theGlobalSettings);

                var theConfig = Configuration.Init();

                if (!theGlobalSettings.Logging.IsActive)
                    return;

                if (theGlobalSettings.Logging.ColoredConsole.IsActive)
                {
                    theConfig.AddColoredConsoleTarget("coloredConsole", theGlobalSettings.Logging.ColoredConsole.MinLogLevel, theGlobalSettings.Logging.ColoredConsole.MaxLogLevel);
                }

                if (theGlobalSettings.Logging.EventLog.IsActive)
                {
                    // ensureEventLogCategoryExists
#pragma warning disable CA1416 // Validate platform compatibility

                    if (!EventLog.Exists(theGlobalSettings.Logging.EventLog.CategoryName))
                    {
                        EventLog.CreateEventSource(theGlobalSettings.Logging.EventLog.SourceName, theGlobalSettings.Logging.EventLog.CategoryName);
                    }

                    if (!EventLog.SourceExists(theGlobalSettings.Logging.EventLog.SourceName))
                    {
                        EventLog.CreateEventSource(theGlobalSettings.Logging.EventLog.SourceName, theGlobalSettings.Logging.EventLog.CategoryName);
                    }

                    theConfig.AddEventLogTarget("eventLogTarget", theGlobalSettings.Logging.EventLog.MinLogLevel, theGlobalSettings.Logging.EventLog.MaxLogLevel, theGlobalSettings.Logging.EventLog.CategoryName, theGlobalSettings.Logging.EventLog.SourceName);
#pragma warning restore CA1416 // Validate platform compatibility
                }

                if (theGlobalSettings.Logging.File.IsActive)
                {
                    theConfig.AddFileTarget("fileTarget", theGlobalSettings.Logging.File.MinLogLevel, theGlobalSettings.Logging.File.MaxLogLevel, theGlobalSettings.Logging.File.IsInAppSameDirectory, theGlobalSettings.Logging.File.AbsoluteDirectoryPath, theGlobalSettings.Logging.File.FileName);
                }

                if (theGlobalSettings.Logging.Database.IsActive)
                {
                    var theConnectionString = configuration.GetConnectionString(ConnectionStringNames.AuditsDatabase);
                    theConfig.AddDatabaseTarget("databaseTarget", theGlobalSettings.Logging.Database.MinLogLevel, theGlobalSettings.Logging.Database.MaxLogLevel, theConnectionString);
                }
            }
        }

        private static void logStartingApplicationInfo(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                _logger.Trace(">> Starting appliaction...");
                //_logger.Info(Models.Enums.AuditHelpers.IdpApplication.Category, Models.Enums.AuditHelpers.IdpApplication.Start);

                // environment info
                var appStartingInfo = $">> Using Environment : {environment.EnvironmentName}";
                appStartingInfo += System.Environment.NewLine;

                // connectionString (password's part removed)
                var theConnectionStringWithoutPasswordText = StartupHelper.GetConnectionStringWithoutPasswordText(configuration, ConnectionStringNames.MainDatabase);
                appStartingInfo += $">> Using ConnectionString : {theConnectionStringWithoutPasswordText}";
                appStartingInfo += System.Environment.NewLine;

                _logger.Info(FinLib.Models.Enums.EventCategory.Application, FinLib.Models.Enums.EventId.Start
                    , FinLib.Models.Enums.EventType.Success, appStartingInfo);
            }
        }

        private static void onApplicationStarted()
        {
            System.Console.WriteLine("Application Started");
            _logger.Trace("Applicaiton Started.");
        }

        private static void onApplicationStopping()
        {
            System.Console.WriteLine("Application Stopping...");
            _logger.Trace("Applicaiton Stopping...");
        }

        private static void onApplicationStopped()
        {
            System.Console.WriteLine("Application Stopped.");
            _logger.Trace("Applicaiton Stopped.");
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var theHost = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // ------------------------
                    // To run Kestrel on LocalNetwork
                    //webBuilder.UseUrls("https://0.0.0.0:5000");
                    //webBuilder.UseKestrel();
                    // -------------------------

                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logOption =>
                {
                    logOption.SetMinimumLevel(LogLevel.Trace);
                    logOption.AddConsole();
                })
                .UseNLog();

            return theHost;
        }
    }
}