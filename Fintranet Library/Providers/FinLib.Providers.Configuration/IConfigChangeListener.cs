namespace FinLib.Providers.Configuration
{
    public interface IConfigChangeListener
    {
        /// <summary>
        /// Fire when the appsettings changed
        /// </summary>
        /// <param name="category">the category name of the changed config</param>
        /// <param name="key">the key name of the changed config</param>
        void ReloadData(string category, string key);
    }
}
