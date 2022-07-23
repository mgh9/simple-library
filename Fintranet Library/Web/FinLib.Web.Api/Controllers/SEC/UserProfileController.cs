using FinLib.Models.Base;
using FinLib.Models.Configs;
using FinLib.Models.Dto.SEC;
using FinLib.Models.Dtos.SEC;
using FinLib.Models.Dtos.SEC.User.Profile;
using FinLib.Models.Enums;
using FinLib.Providers.Identity;
using FinLib.Services.Base;
using FinLib.Services.SEC;
using FinLib.Web.Api.Base;
using FinLib.Web.Shared.Attributes;
using Microsoft.AspNetCore.Mvc;
using JsonResult = FinLib.Models.Base.JsonResult;

namespace FinLib.Web.Api.SEC
{
    public class UserProfileController : BaseController
    {
        private readonly UserProfileService _service;
        private readonly AppUserManager _appUserManager;

        public UserProfileController(ICommonServicesProvider<GlobalSettings> commonServicesContainer
            , AppUserManager appUserManager
            , UserService userService)
            : base(commonServicesContainer)
        {
            this._service = new UserProfileService(commonServicesContainer, userService);
            this._appUserManager = appUserManager;
        }

        [HttpGet]
        [MyAuthorize( ApplicationRole.Customer)]
        public async Task<JsonResult<UserProfileDto, UserProfileConfigDto>> GetAsync()
        {
            var result = new JsonResult<UserProfileDto, UserProfileConfigDto>
            {
                Data = await _service.GetProfileAsync(),
                Success = true
            };

            result.Data.ImageAbsoluteUrl = UserController.GetUserAbsoluteImageUrl(result.Data);
            result.DataConfig = new UserProfileConfigDto()
            {
                ChangePasswordConfig = new ChangePasswordConfigDto
                {
                    PasswordPolicy = CommonServicesProvider.AppSettingsProvider.Identity.PasswordPolicy,
                }
            };

            return result;
        }

        [HttpPut]
        public async Task<JsonResult> UpdateAsync(UserProfileDto model)
        {
            var result = new JsonResult();

            await _service.UpdateAsync(model);
            result.Success = true;

            return result;
        }

        [HttpPut]
        [MyAuthorize(ApplicationRole.Customer)]
        public async Task<JsonResult> ChangePasswordAsync(ChangePasswordDto model)//, [FromServices] ISmsProvider smsProvider)
        {
            var result = new JsonResult();

            var theUserService = new UserService(CommonServicesProvider, _appUserManager);
            await theUserService.ChangePasswordAsync(model);
            result.Success = true;

            return result;
        }
    }
}
