namespace FinLib.Models.Configs.Identity
{
    public sealed class PasswordPolicy
    {
        public const string CategoryKey = "Identity_" + nameof(PasswordPolicy);

        public bool RequireDigit { get; set; }
        public int RequiredLength { get; set; }
        public int MaxLength { get; set; }
        public bool RequireNonAlphanumeric { get; set; }
        public bool RequireUppercase { get; set; }
        public bool RequireLowercase { get; set; }
    }
}
