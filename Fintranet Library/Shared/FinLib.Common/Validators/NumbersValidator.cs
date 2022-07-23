using FinLib.Common.Extensions;

namespace FinLib.Common.Validators
{
    public static class NumbersValidator
    {
        public static bool IsDigitsOnly(string text)
        {
            if (text.IsEmpty())
                return false;

            return text.All(ch => char.IsDigit(ch));
        }
    }
}
