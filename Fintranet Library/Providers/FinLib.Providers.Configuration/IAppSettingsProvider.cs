namespace FinLib.Providers.Configuration
{
    public interface IAppSettingsProvider<out TSettings>
        where TSettings : class, new()
    {
        TSettings Settings { get; }
    }
}
