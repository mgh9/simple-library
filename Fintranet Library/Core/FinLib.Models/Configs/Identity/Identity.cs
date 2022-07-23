using FinLib.Models.Attributes;

namespace FinLib.Models.Configs.Identity
{
    [IgnoreTypewriterMapping]
    public sealed class Identity
    {
        public Lockout Lockout { get; set; }

        public UserNamePolicy UserNamePolicy { get; set; }

        public PasswordPolicy PasswordPolicy { get; set; }

        public bool CanEditProfile { get; set; }

        public bool CanResetPassword { get; set; }
    }
}
