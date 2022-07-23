using FinLib.Models.Base;
using FinLib.Models.Dto.SEC;
using FinLib.Providers.Identity;
using FinLib.Providers.Logging;
using FinLib.Services.Base;
using FinLib.Services.SEC;
using FinLib.Web.Api.Helpers;
using FinLib.Web.Api.SEC;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace FinLib.Web.Api.Account
{
    public class AccountController : Base.BaseController
    {
        private readonly UserService _userService;
        private readonly IAppLogger _appLogger;
        private readonly IDataProtector _dataProtector;

        public AccountController(ICommonServicesProvider<FinLib.Models.Configs.GlobalSettings> commonServicesProvider
            , AppUserManager appUserManager)
            : base(commonServicesProvider)
        {
            _userService = new UserService(CommonServicesProvider, appUserManager);
            _appLogger = commonServicesProvider.AppLogger;
            _dataProtector = commonServicesProvider.DataProtectionProvider.CreateProtector(Shared.Models.Constants.Security.DataProtection.DefaultUserRoleIdKey);
        }

        [HttpGet]
        public async Task<JsonResult<UserInfoDto>> GetLoggedInUserInfoAsync()
        {
            var result = new JsonResult<UserInfoDto>();
            if (IsAuthenticated)
            {
                var theLoggedInUserRoleId = await GetLoggedInUserRoleIdAsync();
                var userInfo = await _userService.GetUserInfoByUserRoleIdAsync(theLoggedInUserRoleId, true);
                userInfo.ImageAbsoluteUrl = UserController.GetUserAbsoluteImageUrl(userInfo);
                userInfo.DefaultUserRoleId = theLoggedInUserRoleId;

                result.Data = userInfo;
                result.Success = true;
            }
            else
            {
                result.Message = "لطفا مجددا وارد سامانه شوید";
                result.Success = false;
            }

            return result;
        }

        [HttpPost]
        public JsonResult<bool> SetDefaultUserRole([FromBody] int newUserRoleId)
        {
            var result = new JsonResult<bool>();

            if (IsAuthenticated)
            {
                var loggedInUserId = GetLoggedInUserId();
                _userService.SetDefaultUserRole(loggedInUserId, newUserRoleId);
                doSetDefaultUserRole(loggedInUserId, newUserRoleId);

                result.Success = true;
            }
            else
                result.Success = false;

            return result;
        }

        /// <summary>
        /// userRole e user e loggedIn shode ro set mikone (taghir mide)
        /// </summary>
        /// <param name="newUserRoleId">ID e role e user. bayad tuye mahdudeye Role haye khodesh bashe</param>
        private void doSetDefaultUserRole(int loggedInUserId, int newUserRoleId)
        {
            var res = HttpContextAccessor.HttpContext.Response;

            // get his UserRoles
            var userRoles = new UserRoleService(CommonServicesProvider).GetByUserId(loggedInUserId);

            // اگه توی محدوده نقش های مجاز (نقشهایی ک این کاربر منتصب شده بهش) هست، مقدار جدید رو
            // توی کوکی ست کن
            if (userRoles.Any(item => item.Id == newUserRoleId))
            {
                var activeUserRoleCookieOptions = UserManagerHelper.GetCookieOptionsForActiveUserRole();

                // delete the old one
                res.Cookies.Delete(Shared.Models.Constants.Security.CookieNames.ActiveUserRole);

                // create a new one
                var newUserRoleIdEncrypted = _dataProtector.Protect(newUserRoleId.ToString());
                res.Cookies.Append(Shared.Models.Constants.Security.CookieNames.ActiveUserRole
                    , newUserRoleIdEncrypted
                    , activeUserRoleCookieOptions);
            }
            else
            {
                //// yani roleID yi k mikhad set she, tuye mahdudeye mojaze khodesh nist. 
                //_appLogger.AuditWarning(Models.Enums.AuditHelpers.Error.Category
                //                                , Models.Enums.AuditHelpers.Error.AccessDenied
                //                                , Models.Enums.EventType.Failure
                //                                , "شناسه نقش جهت تنظیم، غیرمجاز می باشد"
                //                                , customData: newUserRoleId.ToString());
            }
        }
    }
}