using System;
using System.Runtime.CompilerServices;

namespace FinLib.Models.Attributes
{
    /// <summary>
    /// جهت تزئین مقادیر (در واقع پراپرتی های) موجودیت شناسنامه کاربر
    /// 
    /// برای مثال: موجودیت پروفایل کاربر را در نظر بگیرید
    /// پراپرتی های نام و شماره موبایل را فرض می کنیم. اگر بخاهیم این 2 مقدار، بعنوان
    /// Claim
    /// برای کاربر، در نظر گرفته شده و در دیتابیس ذخیره گردد، آن 2 پراپرتی را با
    /// این اتریبیوت، تزئیین میکنیم
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class ClaimPropertyAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title">نام فارسی کلیم</param>
        /// <param name="claimType">در واقع نام کلیم هست. اما برای رعایت استاندارد نام گذاری در کلیم، به این شکل استفاده می شود. اگه صراحتا ذکر بشه، از
        /// این استفاده میشه، در غیر اینصورت از نام انگلیسی پراپرتی پس از تبدیل به استاندارد کلیم، استفاده میشه</param>
        /// <param name="claimPropertyFilter"></param>
        /// <param name="claimPropertyName"></param>
        public ClaimPropertyAttribute(string title, string claimType = null, string claimPropertyFilter = null, [CallerMemberName] string claimPropertyName = null)
        {
            Title = title;
            ClaimPropertyFilter = claimPropertyFilter;
            ClaimPropertyName = claimPropertyName;

            if (claimType != null)
                ClaimType = claimType;
            else
                ClaimType = Common.Helpers.GeneralHelper.ConvertToStandardClaimTypeName(claimPropertyName);
        }

        /// <summary>
        /// پراپرتی ای که کلیم روی اون ست شده
        /// جهت واکشی مقدار اون پراپرتی از آبجکت متناظر خودش، این اسم رو نگه میداریم
        /// </summary>
        public string ClaimPropertyName { get; set; }

        /// <summary>
        /// عنوان فارسی کلیم
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// نام کلیم
        /// نام لاتین جهت استفاده در سیستم دسترسی
        /// </summary>
        public string ClaimType { get; set; }

        /// <summary>
        /// فیلتر برای واکشی مقدار یک کلیم
        /// برای مثال : برای پراپرتی نوع DateTime
        /// میتوان فیلتر گذاشت که فقط بخش تاریخ (و نه ساعت) را واکشی کنیم
        /// </summary>
        public string ClaimPropertyFilter { get; }
    }
}
