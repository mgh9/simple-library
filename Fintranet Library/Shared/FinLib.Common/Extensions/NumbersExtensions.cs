using FinLib.Common.Helpers;

namespace FinLib.Common.Extensions
{
    public static class NumbersExtensions
    {
        public static string ToFarsiNumbers(this string value)
        {
            return NumbersHelper.ToFarsiNumbers(value);
        }
    }
}
