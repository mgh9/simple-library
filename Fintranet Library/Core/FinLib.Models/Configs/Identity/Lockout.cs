﻿using FinLib.Models.Attributes;

namespace FinLib.Models.Configs.Identity
{
    [IgnoreTypewriterMapping]
    public sealed class Lockout
    {
        public const string CategoryKey = "Identity_" + nameof(Lockout);

        public bool AllowedForNewUsers { get; set; }
        public bool IsLockoutAfterMaxFailedAccessAttemptsEnabled { get; set; }
        public int MaxFailedAccessAttempts { get; set; }

        public int LockoutTimeInMinutes { get; set; }
    }
}
