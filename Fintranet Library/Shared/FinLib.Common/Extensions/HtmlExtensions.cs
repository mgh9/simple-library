using FinLib.Common.Helpers;

namespace FinLib.Common.Extensions
{
    public static class HtmlExtensions
    {
        public static string ReplaceBreakLine(this string value)
        {
            return HtmlHelper.ReplaceBreakLine(value);
        }

        public static string WrapNBSP(this string value)
        {
            return HtmlHelper.WrapNBSP(value);
        }
    }
}
