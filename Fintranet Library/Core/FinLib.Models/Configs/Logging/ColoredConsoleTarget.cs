using FinLib.Models.Attributes;

namespace FinLib.Models.Configs.Logging
{
    [IgnoreTypewriterMapping]
    public class ColoredConsoleTarget : LoggingTarget
    {
        public ColoredConsoleTarget ()
        {
            Type = LoggingTargetType.ColoredConsole;
        }
    }
}
