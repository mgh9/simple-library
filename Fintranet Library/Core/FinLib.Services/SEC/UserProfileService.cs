using FinLib.Common.Exceptions.Business;
using FinLib.Common.Extensions;
using FinLib.Common.Validators;
using FinLib.DomainClasses.SEC;
using FinLib.Models.Dtos.SEC.User.Profile;
using FinLib.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace FinLib.Services.SEC
{
    public class UserProfileService : BaseService
    {
        private readonly UserService _userService;

        public UserProfileService(ICommonServicesProvider<FinLib.Models.Configs.GlobalSettings> commonServicesProvider
            , UserService userService)
            : base(commonServicesProvider)
        {
            this._userService = userService;
        }

        /// <summary>
        /// Get Profile info of the current (logged in) user
        /// </summary>
        /// <returns></returns>
        public async Task<UserProfileDto> GetProfileAsync()
        {
            if (!CommonServicesProvider.IsAuthenticated)
                throw new Common.Exceptions.Business.LogoutException("You signed out the system, Please sign-in again");

            return await GetProfileAsync(CommonServicesProvider.LoggedInUserId.Value);
        }

        /// <summary>
        /// Get Profile info of the current (logged in) user
        /// </summary>
        /// <returns></returns>
        public async Task<UserProfileDto> GetProfileAsync(int userId)
        {
            var query = from theUser in CommonServicesProvider.DbContext.Set<User>()
                        where theUser.Id == userId

                        select new UserProfileDto
                        {
                            #region Personal Related
                            FirstName = theUser.FirstName,
                            LastName = theUser.LastName,
                            FullName = theUser.FirstName + " " + theUser.LastName,
                            Mobile = theUser.Mobile,
                            Gender = theUser.Gender,

                            ImageUrl = theUser.ImageUrl ?? "default.png",

                            BirthDate = theUser.BirthDate,

                            #endregion


                            #region Identity Related
                            Id = userId,
                            UserName = theUser.UserName,
                            Email = theUser.Email,
                            LockoutEnd = theUser.LockoutEnd,
                            IsActive = theUser.IsActive,
                            LastLoggedInTime = theUser.LastLoggedInTime,

                            #endregion
                        };

            var result = await query.SingleAsync();

            // his roles
            result.Roles = new UserRoleService(CommonServicesProvider)
                                    .GetRolesOfUser(userId)
                                    .ToList();

            return result;
        }

        public async Task UpdateAsync(UserProfileDto model)
        {
            validateOnUpdate(model);

            var theUser = await _userService.GetByIdAsync(CommonServicesProvider.LoggedInUserId.Value);

            theUser.FirstName = model.FirstName;
            theUser.LastName = model.LastName;
            theUser.Gender = model.Gender;
            theUser.Mobile= model.Mobile;
            theUser.BirthDate= model.BirthDate;

            await _userService.UpdateAsync(theUser);
        }

        private static void validateOnUpdate(UserProfileDto model)
        {
            model.ThrowIfNull();

            if (model.FirstName.IsEmpty())
                throw new BusinessValidationException("Firstname cannot be empty");

            if (model.LastName.IsEmpty())
                throw new BusinessValidationException("Lastname cannot be empty");

            if (!PersonValidator.IsValidMobile(model.Mobile))
                throw new BusinessValidationException("Invalid mobile number");
        }
    }
}
