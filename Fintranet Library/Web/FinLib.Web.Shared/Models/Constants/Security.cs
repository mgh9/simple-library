namespace FinLib.Web.Shared.Models.Constants
{
    public static class Security
    {
        public static class CookieNames
        {
            public const string ApplicationAuthentication = "rgpcxf6k";
            public const string IdentityServer4CheckSession= "rgixqrc";
            public const string ActiveUserRole = "rgacxo.ve";
            public const string Antiforgery = "rgfxkx";
            public const string Session = "rgsxes";
            public const string Captcha = "rgct";
        }

        public static class DataProtection
        {
            public const string DefaultUserRoleIdKey = "FinLib.Web.DefaultUserRole";
        }

        /// <summary>
        /// نام کوئری پارامتر برای روالی که میخایم زمان لاگین کاربر (کلاینت) چک کنیم
        /// که درخواست لاگین (برای مثال) از سمت سامانه میزکاریکپارچه اومده
        /// و نه اینکه مستقیما از داخل خود کلاینت (سامانه) کاربر درخواست لاگین داده باشه
        /// </summary>
        public const string EnsureClientLoginRequestingFromTheUrl_QueryStringParamName = "assertion_nonce";
    }
}
