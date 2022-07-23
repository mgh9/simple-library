using FinLib.Common.Exceptions.Business;
using FinLib.Common.Exceptions.Business.User;
using FinLib.Common.Exceptions.Infra;
using FinLib.Common.Extensions;
using FinLib.DomainClasses.SEC;
using Microsoft.AspNetCore.Identity;

namespace FinLib.Services.SEC
{
    public partial class UserService
    {
        public async Task<bool> IsUserLockedOutAsync(int userId)
        {
            var theUserEntity = await FindEntityAndThrowExceptionIfNotFoundAsync(userId);
            return await _appUserManager.IsLockedOutAsync(theUserEntity);
        }

        /// <summary>
        /// قفل (غیرفعال سازی) کاربر
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task LockoutByAdminAsync(int userId)
        {
            try
            {
                var theUserEntity = await FindEntityAndThrowExceptionIfNotFoundAsync(userId);

                await lockoutByAdminAsync(theUserEntity);

                // auditing
                CommonServicesProvider.AppLogger.Info(Models.Enums.EventCategory.UserAccountManagement
                    , Models.Enums.EventId.UserLockoutByAdmin, Models.Enums.EventType.Success
                    , customData: userId.ToJson());
            }
            catch (EntityNotFoundException notfoundEx)
            {
                CommonServicesProvider.AppLogger.Error
                    (Models.Enums.EventCategory.UserAccountManagement
                    , Models.Enums.EventId.UserLockoutByAdmin
                    , Models.Enums.EventType.Error
                    , notfoundEx.Message, exception:notfoundEx, customData:userId.ToJson());

                throw;
            }
        }

        public async Task UnlockAsync(int userId)
        {
            try
            {
                var theUserEntity = await FindEntityAndThrowExceptionIfNotFoundAsync(userId);

                await unlockAsync(theUserEntity);

                // auditing
                CommonServicesProvider.AppLogger.Info(
                    Models.Enums.EventCategory.EntityManagement
                    , Models.Enums.EventId.UserUnlockByAdmin
                    , Models.Enums.EventType.Success,customData: userId.ToJson());
            }
            catch (EntityNotFoundException notfoundEx)
            {
                // auditing
                CommonServicesProvider.AppLogger.Error( Models.Enums.EventCategory.UserAccountManagement
                    ,  Models.Enums.EventId.UserUnlockByAdmin, Models.Enums.EventType.Failure
                    , message: notfoundEx.Message, exception: notfoundEx, customData:userId.ToJson());

                throw;
            }
        }

        /// <summary>
        /// کاربر رو براساس تعداد دقیقه ای که در تنظیمات تعریف شده، قفل میکنه
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task lockoutByAdminAsync(User model)
        {
            model.LockoutDescription = $"the account has been lockedout by at {DateTime.Now}";
            model.UpdateDate = DateTime.Now;

            IdentityResult result = await _appUserManager.UpdateAsync(model);

            if (!result.Succeeded)
            {
                throw new UserUnlockException(model.Id, result.GetAllErrors());
            }
        }

        private async Task unlockAsync(User model)
        {
            var unlockingResult = await _appUserManager.SetLockoutEndDateAsync(model, null);
            if (!unlockingResult.Succeeded)
            {
                throw new UserUnlockException(model.Id, unlockingResult.GetAllErrors());
            }

            model.LockoutDescription = null;
            model.UpdateDate = DateTime.Now;
            model.AccessFailedCount = 0;

            await _appUserManager.UpdateAsync(model);
        }
    }
}