using FinLib.Models.Attributes;

namespace FinLib.Models.Configs.Logging
{
    [IgnoreTypewriterMapping]
    public class DatabaseTarget : LoggingTarget
    {
        public DatabaseTarget()
        {
            Type = LoggingTargetType.Database;
        }

        public string ConnectionstringName { get; set; }
    }
}
