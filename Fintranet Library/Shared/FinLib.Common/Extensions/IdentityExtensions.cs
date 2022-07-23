using Microsoft.AspNetCore.Identity;

namespace FinLib.Common.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetAllErrors(this IdentityResult value, string seperator)
        {
            return string.Join(seperator, value.Errors.Select(x => x.Description));
        }

        public static string GetAllErrors(this IdentityResult value)
        {
            return value.GetAllErrors(Environment.NewLine);
        }

        public static IEnumerable<string> GetAllErrorsList(this IdentityResult value)
        {
            return value.Errors.Select(x=> x.Description);
        }
    }
}
