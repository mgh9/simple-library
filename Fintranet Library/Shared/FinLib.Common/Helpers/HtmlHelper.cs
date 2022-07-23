namespace FinLib.Common.Helpers
{
    public static class HtmlHelper
    {
        public static string ReplaceBreakLine(string value)
        {
            return value?.Replace("\n", "<br/>");
        }

        public static string WrapNBSP(string value)
        {
            return $"&nbsp;{value}&nbsp;";
        }
    }
}
