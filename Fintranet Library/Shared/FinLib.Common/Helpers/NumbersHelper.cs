namespace FinLib.Common.Helpers
{
    public static class NumbersHelper
    {
        public static string ToFarsiNumbers(string value)
        {
            return value?
                    .Replace('0', '۰')
                    .Replace('1', '۱')
                    .Replace('2', '۲')
                    .Replace('3', '۳')
                    .Replace('4', '۴')
                    .Replace('5', '۵')
                    .Replace('6', '۶')
                    .Replace('7', '۷')
                    .Replace('8', '۸')
                    .Replace('9', '۹');
        }
    }
}
