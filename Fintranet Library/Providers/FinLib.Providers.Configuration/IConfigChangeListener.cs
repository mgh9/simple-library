namespace FinLib.Providers.Configuration
{
    public interface IConfigChangeListener
    {
        /// <summary>
        /// زمانی که کانفیگی تغییر کنه، این متد فایر میشه
        /// </summary>
        /// <param name="category">the category name of the changed config</param>
        /// <param name="key">the key name of the changed config</param>
        void ReloadData(string category, string key);
    }
}
