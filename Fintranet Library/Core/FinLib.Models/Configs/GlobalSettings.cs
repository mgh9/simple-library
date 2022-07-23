using FinLib.Models.Attributes;

namespace FinLib.Models.Configs
{
    [IgnoreTypewriterMapping]
    public class GlobalSettings
    {
        public Logging.Logging Logging { get; set; }

        public Configs.Identity.Identity Identity { get; set; }

        public List<SeedUser> SeedUsers { get; set; }
    }
}
