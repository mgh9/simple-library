using FinLib.Common.Extensions;
using FinLib.DomainClasses.SEC;
using FinLib.Models.Base;
using FinLib.Models.Configs.Identity;
using FinLib.Models.Dto.SEC;
using FinLib.Models.Dtos.SEC;
using FinLib.Models.Dtos.SEC.User.Profile;
using FinLib.Models.Enums;
using FinLib.Models.SearchFilters.SEC;
using FinLib.Models.Views.SEC;
using FinLib.Services.SEC;
using FinLib.Web.Api.Base;
using FinLib.Web.Shared.Attributes;
using Microsoft.AspNetCore.Mvc;
using JsonResult = FinLib.Models.Base.JsonResult;

namespace FinLib.Web.Api.SEC
{
    public partial class UserController : UpdatableEntityController<User, UserDto, UserView, UserService, UserSearchFilter>
    {
        [HttpGet]
        [MyAuthorize(ApplicationRole.Customer)]
        public virtual async Task<JsonResult<List<TitleValue<int>>>> GetCustomersUserRoleIdsTitleValueListAsync()
        {
            var result = new JsonResult<List<TitleValue<int>>>
            {
                Data = await Service.GetCustomersUserRoleIdsTitleValueListAsync(),
                Success = true
            };

            return result;
        }
        
        [HttpGet]
        public JsonResult<PasswordPolicy> GetPasswordPolicy()
        {
            var result = new JsonResult<PasswordPolicy>
            {
                Data = CommonServicesProvider.AppSettingsProvider.Identity.PasswordPolicy,
                Success = true
            };

            return result;
        }

        [NonAction]
        public async Task<int> CreateWithDefaultRolesAsync(UserDto model)
        {
            ValidateOnInsert(model);

            var createdUserId = await Service.CreateWithDefaultRolesAsync(model);

            return createdUserId;
        }

        [HttpPost]
        [MyAuthorize(ApplicationRole.Admin)]
        public async Task<JsonResult> ResetPasswordAsync(ResetPasswordDto model)
        {
            var result = new JsonResult();

            await Service.ResetPasswordAsync(model);
            result.Success = true;

            return result;
        }

        [HttpPut]
        public async Task<JsonResult> ChangePasswordAsync(ChangePasswordDto model)
        {
            var result = new JsonResult();

            await Service.ChangePasswordAsync(model);
            result.Success = true;

            return result;
        }

        [HttpDelete]
        [MyAuthorize(ApplicationRole.Admin)]
        public async Task<JsonResult<int>> RemoveUserRoleAsync([FromBody] int userRoleId)
        {
            var result = new JsonResult<int>
            {
                Success = true
            };
            await new UserRoleService(CommonServicesProvider).DeleteAsync(userRoleId);

            return result;
        }

        [HttpPut]
        [MyAuthorize(ApplicationRole.Admin)]
        public async Task<JsonResult> SaveUserRolesAsync(UserDto model)
        {
            await Service.SaveUserRolesAsync(model);

            var result = new JsonResult
            {
                Success = true,
            };

            return result;
        }

        public static string GetUserAbsoluteImageUrl(UserProfileDto userProfile)
        {
            if (userProfile.ImageUrl.IsEmpty())
            {
                return Shared.Models.Constants.UploadsUrls.UserImage.Default;
            }
            else
            {
                return $"{Shared.Models.Constants.UploadsUrls.UserImage.BaseUrl}/{userProfile.ImageUrl}";
            }
        }

        public static string GetUserAbsoluteImageUrl(UserInfoDto userInfo)
        {
            if (userInfo.ImageUrl.IsEmpty())
            {
                return Shared.Models.Constants.UploadsUrls.UserImage.Default;
            }
            else
            {
                return $"{Shared.Models.Constants.UploadsUrls.UserImage.BaseUrl}/{userInfo.ImageUrl}";
            }
        }
    }
}
