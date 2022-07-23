using FinLib.Models.Attributes;

namespace FinLib.Models.Configs.Identity
{
    public sealed class ChangePassword
    {
        public const string CategoryKey = "Identity_" + nameof(ChangePassword);

        public bool CanUserChangeHisPassword { get; set; }
    }
}
