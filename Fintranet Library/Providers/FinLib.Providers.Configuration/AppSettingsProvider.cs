using Microsoft.Extensions.Options;

namespace FinLib.Providers.Configuration
{
    public class AppSettingsProvider<TSettings> : IAppSettingsProvider<TSettings>
        where TSettings : class, new()
    {
        public AppSettingsProvider(IOptionsMonitor<TSettings> optionsMonitor)
        {
            Settings = optionsMonitor.CurrentValue;

            optionsMonitor.OnChange(listener =>
            {
                Settings = listener;
            });
        }

        public TSettings Settings { get; private set; }
    }
}
