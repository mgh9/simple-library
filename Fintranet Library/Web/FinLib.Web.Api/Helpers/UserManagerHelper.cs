using FinLib.Common.Exceptions.Business;
using FinLib.Common.Exceptions.Business.User;
using FinLib.Common.Extensions;
using FinLib.DataLayer.Context;
using FinLib.Models.Dtos.SEC;
using FinLib.Services.SEC;
using Microsoft.AspNetCore.DataProtection;

namespace FinLib.Web.Api.Helpers
{
    public static class UserManagerHelper
    {
        internal static CookieOptions GetCookieOptionsForActiveUserRole()
        {
            return new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1),
                HttpOnly = true,
                Secure = true,

                SameSite = SameSiteMode.Strict //SameSiteMode.Lax // SOC : ~> Strict
            };
        }

        public static async Task<int> GetLoggedInUserRoleIdAsync(IHttpContextAccessor httpContextAccessor, bool onlyActiveRoles)
        {
            if (!httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new LogoutException("لطفا مجددا وارد سامانه شوید");
            }

            var dbContext = (IUnitOfWork)httpContextAccessor.HttpContext.RequestServices.GetService(typeof(AppDbContext));
            var dataProtectionProvider = (IDataProtector)httpContextAccessor.HttpContext.RequestServices.GetService(typeof(IDataProtectionProvider));
            var dataProtector = dataProtectionProvider.CreateProtector(Shared.Models.Constants.Security.DataProtection.DefaultUserRoleIdKey);

            var loggedInUserId = httpContextAccessor.HttpContext.User.GetLoggedInUserId<int>();
            var userRolesOfAuthenticatedUser = await getUserRolesOfUserAsync(dbContext, loggedInUserId, onlyActiveRoles);
            var hisDefaultUserRoleId = getDefaultRoleOfUser(userRolesOfAuthenticatedUser);

            var currentRequest = httpContextAccessor.HttpContext.Request;
            var currentResponse = httpContextAccessor.HttpContext.Response;

            return adjustActiveUserRoleCookie(currentRequest, currentResponse, hisDefaultUserRoleId, dataProtector, userRolesOfAuthenticatedUser);
        }

        private static int adjustActiveUserRoleCookie(HttpRequest request, HttpResponse response, int hisDefaultUserRoleId, IDataProtector dataProtector, List<UserRoleDto> userRolesOfAuthenticatedUser)
        {
            var loggedInUserRoleId = -1;

            var activeUserRole_CookieValue = request.Cookies[Shared.Models.Constants.Security.CookieNames.ActiveUserRole];

            // age cookie vojud nadasht ~> taaze login karde ~> cookie (UserRoleId) ro set kon
            if (activeUserRole_CookieValue.IsEmpty())
            {
                var activeUserRoleCookieOptions = GetCookieOptionsForActiveUserRole();

                loggedInUserRoleId = hisDefaultUserRoleId;
                var loggedInUserRoleIdEncrypyed = dataProtector.Protect(loggedInUserRoleId.ToString());

                response.Cookies.Append(
                    Shared.Models.Constants.Security.CookieNames.ActiveUserRole,
                    loggedInUserRoleIdEncrypyed,
                    activeUserRoleCookieOptions);
            }
            else
            {
                // loggedInUserRoleId = Convert.ToInt32(activeUserRole_CookieValue);
                var loggedInUserRoleIdEncrypted = activeUserRole_CookieValue;
                loggedInUserRoleId = Convert.ToInt32(dataProtector.Unprotect(loggedInUserRoleIdEncrypted));

                if (!userRolesOfAuthenticatedUser.Any(item => item.Id == loggedInUserRoleId))
                {
                    loggedInUserRoleId = hisDefaultUserRoleId;
                    var loggedInUserRoleIdEncrypyed = dataProtector.Protect(loggedInUserRoleId.ToString());

                    var activeUserRoleCookieOptions = GetCookieOptionsForActiveUserRole();

                    response.Cookies.Append(
                        Shared.Models.Constants.Security.CookieNames.ActiveUserRole,
                        loggedInUserRoleIdEncrypyed,
                        activeUserRoleCookieOptions);
                }
            }

            return loggedInUserRoleId;
        }

        private static int getDefaultRoleOfUser(List<UserRoleDto> userRolesOfAuthenticatedUser)
        {
            try
            {
                return userRolesOfAuthenticatedUser.Single(item => item.IsDefault).Id;
            }
            catch (Exception ex)
            {
                throw new Common.Exceptions.Infra.FatalException("خطا در تعیین شناسه کاربری پیش فرض کاربر رخ داده است", ex);
            }
        }

        private static async Task<List<UserRoleDto>> getUserRolesOfUserAsync(IUnitOfWork dbContext, int userId, bool onlyActiveRoles)
        {
            var hisUserRoles = await UserService.GetUserRolesInfoAsync(dbContext, userId, onlyActiveRoles);

            if (hisUserRoles.Count == 0)
            {
                throw new InvalidUserRoleException(userId, "برای کاربری شما، نقش منتصب تعریف نشده است!");
            }

            var properedUserRoles = getUserRolesPrepared(dbContext, hisUserRoles);

            return properedUserRoles;
        }

        private static List<UserRoleDto> getUserRolesPrepared(IUnitOfWork dbContext, List<UserRoleDto> userRoles)
        {
            var countOfHisDefaultRoles = userRoles.Count(x => x.IsDefault);

            // age 1 role e default dare k ok e hame chi
            if (countOfHisDefaultRoles == 1)
            {
                return userRoles;
            }

            // yani hich Default i nadashte bashe, ya bishtar az 1 baashe
            // ~> First e sh ro set kon b onvane default
            var defaultUserRole = userRoles[0];
            UserService.SetDefaultUserRole(dbContext, defaultUserRole.UserId, defaultUserRole.Id);

            userRoles.ForEach(x => x.IsDefault = false);
            defaultUserRole.IsDefault = true;

            return userRoles;
        }
    }
}
