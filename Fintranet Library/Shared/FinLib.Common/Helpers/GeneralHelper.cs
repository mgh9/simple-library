using System;
using System.Text.RegularExpressions;

namespace FinLib.Common.Helpers
{
    public static class GeneralHelper
    {
        /// <summary>
        /// تبدیل نام انگلیسی پراپرتی بصورت استاندارد برای 
        /// Claim
        /// برای مثال : کد ملی که بصورت
        /// NationalCode
        /// تعریف شده است، تبدیل می شود به
        /// national_code
        /// </summary>
        /// <param name="claimName"></param>
        /// <returns></returns>
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "<Pending>")]
        public static string ConvertToStandardClaimTypeName(string claimName)
        {
            // ex: NationalCode ~> national_code
            var retval = Regex.Replace(claimName, @"(?<!_)([A-Z])", "_$1")
                            .TrimStart('_')
                            .ToLowerInvariant();    /* we need LowerInvariant not Upper : user_name not USER_NAME */

            return retval;
        }

        /// <summary>
        /// مقدار اثر انگشت گواهی امنیتی رو تر و تمیز میکنه جهت جستجو در مخزن گواهینامه ها
        /// </summary>
        /// <param name="thumbprint"></param>
        /// <returns></returns>
        public static string CleanCertificateThumprint(string thumbprint)
        {
            return Regex.Replace(thumbprint, @"\s|\W", "").ToUpper();
        }
    }
}
