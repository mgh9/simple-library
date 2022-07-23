using NLog;
using NLog.Config;
using FinLib.Providers.Logging.CustomLayoutRenderers;

namespace FinLib.Providers.Logging
{
    public static class Configuration
    {
        static Configuration()
        {

        }

        public static LoggingConfiguration Init()
        {
            // throw if NLog configs have any errors
            LogManager.ThrowExceptions = true;
            LogManager.ThrowConfigExceptions = true;

            // finding the right Assembly , vaghti k StackTrace ro mikhad sabt kone
            LogManager.AddHiddenAssembly(typeof(Configuration).Assembly);

            // need for "{aspnet-user-id} CustomLayout" ! so dirty!
            var assembly = typeof(AspNetUserIdLayoutRenderer).Assembly;
            ConfigurationItemFactory.Default.RegisterItemsFromAssembly(assembly);

            // config entity            
            var theConfig = new LoggingConfiguration();

            // register my LayoutRenderer
            registerCustomLayoutRenderers();

            // TODO: use ElasticSearch for browsing

            // apply
            LogManager.Configuration = theConfig;

            LogManager.ConfigurationReloaded += (sender, e) =>
            {
                //Re apply if config reloaded
                Init();
            };

            return LogManager.Configuration;
        }

        public static LoggingConfiguration LoggingConfiguration { get { return LogManager.Configuration; } }

        private static void registerCustomLayoutRenderers()
        {
            // شناسه ی کاربر لاگین شده
            LogManager.Setup().SetupExtensions(s => s.RegisterLayoutRenderer<AspNetUserIdLayoutRenderer>("aspnet-user-id"));

            // تمام هدرها در درخواست
            LogManager.Setup().SetupExtensions(s => s.RegisterLayoutRenderer<AspNetRequestAllHeadersLayoutRenderer>("aspnet-request-all-headers"));
        }
    }
}
