using FinLib.Models.Attributes;

namespace FinLib.Models.Configs.Logging
{
    [IgnoreTypewriterMapping]
    public class FileTarget : LoggingTarget
    {
        public FileTarget()
        {
            Type = LoggingTargetType.File;
        }

        public bool IsInAppSameDirectory { get; set; }
        public string AbsoluteDirectoryPath { get; set; }
        public string FileName { get; set; }
    }
}
