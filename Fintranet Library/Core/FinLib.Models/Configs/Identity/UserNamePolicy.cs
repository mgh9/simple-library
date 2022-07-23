using FinLib.Models.Attributes;

namespace FinLib.Models.Configs.Identity
{
    [IgnoreTypewriterMapping]
    public sealed class UserNamePolicy
    {
        public const string CategoryKey = "Identity_" + nameof(UserNamePolicy);

        public int RequiredLength { get; set; }
        public int MaxLength { get; set; }
    }
}
