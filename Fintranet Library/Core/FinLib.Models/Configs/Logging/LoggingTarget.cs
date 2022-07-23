using FinLib.Models.Attributes;
using NLog;

namespace FinLib.Models.Configs.Logging
{
    [IgnoreTypewriterMapping]
    public class LoggingTarget
    {
        public bool IsActive { get; set; }
        public LoggingTargetType Type { get; set; }
        public LogLevel MinLogLevel { get; set; }
        public LogLevel MaxLogLevel { get; set; }
    }
}
