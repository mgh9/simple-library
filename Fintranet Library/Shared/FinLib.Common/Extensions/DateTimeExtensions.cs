using FinLib.Common.Helpers;
using System;

namespace FinLib.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToPersianDate(this DateTime? dateTime)
        {
            return DateTimeHelper.ToPersianDate(dateTime);
        }

        public static string ToPersianDate(this DateTime dateTime)
        {
            return DateTimeHelper.ToPersianDate(dateTime);
        }

        public static string ToPersianDateTime(this DateTime? dateTime)
        {
            return DateTimeHelper.ToPersianDateTime(dateTime);
        }

        public static string ToPersianDateTime(this DateTime dateTime)
        {
            return DateTimeHelper.ToPersianDateTime(dateTime);
        }

        public static int GetCurrentPersianMonthNumber()
        {
            return DateTimeHelper.GetCurrentPersianMonthNumber();
        }

        public static DateTime? ToGregorianDate(this string persianDate, bool includeTime = false)
        {
            return DateTimeHelper.ToGregorianDate(persianDate, includeTime);
        }
    }
}
