using System;
using System.Security.Claims;

namespace FinLib.Common.Extensions
{
    public static class UserManagerExtensions
    {
        /// <summary>
        /// شناسه ی کاربر لاگین شده رو برمیگردونه
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static T GetLoggedInUserId<T>(this ClaimsPrincipal principal)
        {
            principal.ThrowIfNull();

            var loggedInUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(loggedInUserId, typeof(T));
            }
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(long))
            {
                return loggedInUserId != null ? (T)Convert.ChangeType(loggedInUserId, typeof(T)) : (T)Convert.ChangeType(0, typeof(T));
            }
            else
            {
                throw new Common.Exceptions.Infra.FatalException("Invalid UserPK type provided");
            }
        }

        /// <summary>
        /// نام کاربری کاربر لاگین شده رو برمیگردونه
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetLoggedInUserName(this ClaimsPrincipal principal)
        {
            principal.ThrowIfNull();

            return principal.FindFirstValue(ClaimTypes.Name);
        }

        /// <summary>
        /// ایمیل کاربر لاگین شده رو برمیگردونه
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetLoggedInUserEmail(this ClaimsPrincipal principal)
        {
            principal.ThrowIfNull();

            return principal.FindFirstValue(ClaimTypes.Email);
        }
    }
}
