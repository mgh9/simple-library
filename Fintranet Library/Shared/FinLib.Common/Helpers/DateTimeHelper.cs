using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FinLib.Common.Helpers
{
    public static class DateTimeHelper
    {
        public static string ToPersianDate(DateTime? dateTime)
        {
            return !dateTime.HasValue ? null : ToPersianDate(dateTime.Value, false);
        }

        public static string ToPersianDate(DateTime dateTime, bool includeTime = false)
        {
            if (dateTime == DateTime.MinValue)
                throw new InvalidTimeZoneException("مقدار تاریخ/زمان نامعتبر می باشد");

            var calendar = new PersianCalendar();

            if (!includeTime)
                return $"{calendar.GetYear(dateTime):0000}/{calendar.GetMonth(dateTime):00}/{calendar.GetDayOfMonth(dateTime):00}";

            return $"{calendar.GetYear(dateTime):0000}/{calendar.GetMonth(dateTime):00}/{calendar.GetDayOfMonth(dateTime):00} {calendar.GetHour(dateTime):00}:{calendar.GetMinute(dateTime):00}";
        }

        public static string ToPersianDateTime(DateTime? dateTime)
        {
            return !dateTime.HasValue ? null : ToPersianDate(dateTime.Value, true);
        }

        public static string ToPersianDateTime(DateTime dateTime)
        {
            return ToPersianDate(dateTime, true);
        }

        public static int GetCurrentPersianMonthNumber()
        {
            var currentPersianDate = ToPersianDate(DateTime.Now);
            int persianMonthNumber = Convert.ToInt32(currentPersianDate.Split('/')[1]);
            return persianMonthNumber;
        }

        public static int GetCurrentPersianYearNumber()
        {
            var currentPersianDate = ToPersianDate(DateTime.Now);
            int persianYearNumber = Convert.ToInt32(currentPersianDate.Split('/')[0]);
            return persianYearNumber;
        }

        public static string GetCurrentPersianDate()
        {
            return ToPersianDate(DateTime.Now);
        }

        public static string GetCurrentPersianDateTime()
        {
            return ToPersianDateTime(DateTime.Now);
        }

        public static DateTime? ToGregorianDate(string persianDate, bool includeTime = false)
        {
            if (string.IsNullOrWhiteSpace(persianDate))
                return null;

            var calendar = new PersianCalendar();

            Match match;
            if (includeTime)
                match = Regex.Match(persianDate, @"^(\d+)/(\d+)/(\d+) (\d{2}):(\d{2})$");
            else
                match = Regex.Match(persianDate, @"^(\d+)/(\d+)/(\d+)$");

            var year = int.Parse(match.Groups[1].Value);
            var month = int.Parse(match.Groups[2].Value);
            var day = int.Parse(match.Groups[3].Value);
            var hour = 0;
            var minute = 0;

            if (includeTime)
            {
                hour = int.Parse(match.Groups[4].Value);
                minute = int.Parse(match.Groups[5].Value);
            }

            return calendar.ToDateTime(year, month, day, hour, minute, 0, 0);
        }

        /// <summary>
        /// مدت زمان ورودی رو بصورت فارسی "کاربر پسند" تبدیل می کنه
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns>مثال: 3 روز و 10 ساعت و 20 دقیقه و 45 ثانیه</returns>
        public static string GetTotalTimeAsUserFriendly(TimeSpan timeSpan)
        {
            string retval;

            var totalDays = (int)Math.Abs(timeSpan.TotalDays);
            var totalHours = (int)Math.Abs(timeSpan.TotalHours);
            var totalMinutes = (int)Math.Abs(timeSpan.TotalMinutes);

            if (totalDays > 0)
            {
                retval = $"{totalDays} روز و {timeSpan.Hours} ساعت و {timeSpan.Minutes} دقیقه و {timeSpan.Seconds} ثانیه";
            }
            else if (totalHours > 0)
            {
                retval = $"{totalHours} ساعت و {timeSpan.Minutes} دقیقه و {timeSpan.Seconds} ثانیه";
            }
            else
            {
                retval = $"{totalMinutes} دقیقه و {timeSpan.Seconds} ثانیه";
            }

            return retval;
        }

        public static string GetCurrentGregorianDateAsFileName()
        {
            var now = DateTime.Now;

            var result = $"{now.Year}{now.Month}{now.Day} {now.Hour}{now.Minute}{now.Second}";
            return result;
        }
    }
}
