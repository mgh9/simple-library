using FinLib.Models.Attributes;

namespace FinLib.Models.Configs.Logging
{
    [IgnoreTypewriterMapping]
    public sealed class Logging
    {
        public const string CategoryKey = nameof(Logging);

        public bool IsActive { get; set; }

        public EventLogTarget EventLog { get; set; }
        public DatabaseTarget Database { get; set; }
        public FileTarget File { get; set; }
        public ColoredConsoleTarget ColoredConsole { get; set; }
    }
}
