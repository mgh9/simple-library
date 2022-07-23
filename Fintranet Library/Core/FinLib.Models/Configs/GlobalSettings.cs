using FinLib.Models.Attributes;

namespace FinLib.Models.Configs
{
    /// <summary>
    /// تنظیمات عمومی در برنامه
    /// </summary>
    [IgnoreTypewriterMapping]
    public class GlobalSettings
    {
        public Logging.Logging Logging { get; set; }

        public Configs.Identity.Identity Identity { get; set; }

        public List<SeedUser> SeedUsers { get; set; }
    }
}
