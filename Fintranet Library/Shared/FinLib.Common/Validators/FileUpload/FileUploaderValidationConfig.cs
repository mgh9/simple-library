using System.Collections.Generic;

namespace FinLib.Common.Validators.FileUpload
{
    public class FileUploaderValidationConfig
    {
        /// <summary>
        /// حداقل سایز نام فایل
        /// </summary>
        public int MinFileNameLength { get; set; }

        /// <summary>
        /// حداکثر سایز نام فایل
        /// </summary>
        public int MaxFileNameLength { get; set; }

        /// <summary>
        /// رشته ی رگولار اکسپرشنی که باهاش، نام فایل رو ولیدیت کنیم. پیش فرض: نام فایل حاوی عدد و حروف انگلیسی فقط باشه
        /// </summary>
        public string RegexToValidateFileName { get; set; } = @"^[a-zA-Z0-9]{1,200}\.[a-zA-Z0-9]{1,10}$";

        public int MinFileSizeInBytes { get; set; } = 10 * 1024; // 10KB
        public int MaxFileSizeInMB { get; set; } = 5;

        /// <summary>
        /// حداقل تعداد تناقضی که از بین کلمات ممنوعه، در محتوای فایل پیدا کنه و اگه بیش از این باشه، فایل رو نامعتبر در نظر بگیره
        /// </summary>
        public int MaxAllowedForbiddenWordsInFileContent { get; set; } = 1;

        /// <summary>
        /// مسیر کامل فایل اجرایی مربوط به اسکن فایل ها توسط انتی ویروس که در محیط سیستم عامل نصب هست
        /// </summary>
        public string AntivirusCommandLineFileName { get; set; }

        /// <summary>
        /// پارامترهایی (آرگومان هایی) که جهت اسکن فایل توسط آنتی ویروس نصب شده در سیستم عامل هست
        /// </summary>
        public string AntivirusCommandLineArguments { get; set; }

        /// <summary>
        /// اگه فعال باشه، و اگه ک فایل مورد اپلود، عکس باشه، اون رو در همین ابعادی ک هست سعی میکنه ریسایز کنه و بازنویسی کنه، تا با این شیوه
        /// برخی از کدهای مخرب اگه تووش تزریق شده رو بشه از بین برد. البته که باز هم قابل دور زدن هست!
        /// </summary>
        public bool FakeResizeAndReplaceIfImage { get; set; }

        /// <summary>
        /// پیامی که در صورت ناموفق بودن اعتبارسنجی برمیگردد
        /// </summary>
        public string ValidationFailedMessage { get; set; } = "فایل انتخابی نامعتبر می باشد";

        public HashSet<string> AllowedFileExtensions { get; set; } = new HashSet<string>();

        public HashSet<string> ForbiddenWordsInContent { get; set; } = new  HashSet<string>();
    }
}
