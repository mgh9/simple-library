using FinLib.Models.Attributes;

namespace FinLib.Models.Configs.Logging
{
    [IgnoreTypewriterMapping]
    public class EventLogTarget : LoggingTarget
    {
        public EventLogTarget()
        {
            Type = LoggingTargetType.EventLog;
        }

        public string CategoryName { get; set; }
        public string SourceName { get; set; }
    }
}
