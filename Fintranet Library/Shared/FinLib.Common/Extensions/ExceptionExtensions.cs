using FinLib.Common.Exceptions.Base;
using FinLib.Common.Exceptions.Infra;
using System.Text;

namespace FinLib.Common.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetWholeMessage(this Exception value, int nestedInnerMessages = 5)
        {
            value.ThrowIfNull();

            var retval = new StringBuilder();

            retval.AppendLine(value.Message);
            while (value.InnerException != null && nestedInnerMessages > 0)
            {
                retval.AppendLine();
                retval.AppendLine();
                retval.AppendLine("# >> Inner : " + value.InnerException.Message + "#");

                value = value.InnerException;
                nestedInnerMessages--;
            }

            retval.AppendLine();
            retval.AppendLine();
            retval.AppendLine("# StackTrace :");
            retval.AppendLine();
            retval.AppendLine(value.StackTrace);
            retval.AppendLine();
            retval.AppendLine("#");

            return retval.ToString();
        }

        public static void ThrowIfNull(this object value, string message = null)
        {
            if (value is null)
                throw new NullModelException(nameof(value), message);
        }

        /// <summary>
        /// Check if this exception is any business-related exceptions or not?
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsBusinessRelatedException(this Exception value)
        {
            value.ThrowIfNull();

            return value is BaseBusinessException;
        }
    }
}
