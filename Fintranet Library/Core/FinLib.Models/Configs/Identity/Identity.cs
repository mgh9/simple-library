using FinLib.Models.Attributes;

namespace FinLib.Models.Configs.Identity
{
    [IgnoreTypewriterMapping]
    public sealed class Identity
    {
        /// <summary>
        /// قوانین مربوط به قفل شدن (غیرفعال شدن) حساب کاربری
        /// </summary>
        public Lockout Lockout { get; set; }

        /// <summary>
        /// قوانین نام کاربری
        /// </summary>
        public UserNamePolicy UserNamePolicy { get; set; }

        /// <summary>
        /// قوانین رمز عبور
        /// </summary>
        public PasswordPolicy PasswordPolicy { get; set; }

        /// <summary>
        /// آیا کاربر بتواند، اطلاعات پروفایل خودش را اپدیت کند؟
        /// </summary>
        public bool CanEditProfile { get; set; }

        /// <summary>
        /// آیا کاربر بتواند رمز عبور خود را ریست (تنظیم مجدد) کند؟
        /// </summary>
        public bool CanResetPassword { get; set; }

        ///// <summary>
        ///// حداکثر سایز فایل تصویر کاربر
        ///// </summary>
        //public int MaxImageFileSizeInMB { get; set; }
    }
}
