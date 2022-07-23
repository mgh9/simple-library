using FinLib.Common.Exceptions.Base;
using FinLib.Common.Exceptions.Business;
using FinLib.Common.Extensions;
using FinLib.Models.Dtos.SEC;
using System.Transactions;

namespace FinLib.Services.SEC
{
    public partial class UserService
    {
        public async Task ResetPasswordAsync(ResetPasswordDto model)
        {
            int subjectUserId;
            string subjectUserName;
            string newPassword;

            using (var theTransaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    validateOnResetPassword(model);

                    var theUserEntity = await FindEntityAndThrowExceptionIfNotFoundAsync(model.SubjectUserId);
                    subjectUserId = theUserEntity.Id;
                    subjectUserName = theUserEntity.UserName;
                    newPassword = model.NewPassword;

                    // update user's password in db
                    var generatePasswordResetToken = await _appUserManager.GeneratePasswordResetTokenAsync(theUserEntity);
                    var resetPasswordResult = await _appUserManager.ResetPasswordAsync(theUserEntity, generatePasswordResetToken, model.NewPassword);
                    if (!resetPasswordResult.Succeeded)
                    {
                        throw new GeneralBusinessLogicException(resetPasswordResult.GetAllErrors());
                    }

                    // 
                    await _appUserManager.ResetAccessFailedCountAsync(theUserEntity);

                    // user info changed time, refreshed
                    await _appUserManager.RefreshUpdateTimeAsync(theUserEntity);

                    theTransaction.Complete();
                }
                catch (BaseBusinessException bex)
                {
                    theTransaction.Dispose();

                    //CommonServicesProvider.LoggingService.AuditError(new UserPasswordResetEvent(model.SubjectUserId
                    //                                                                            , Models.Enums.EventType.Error)
                    //                                                    , exception: bex);

                    throw;
                }
                catch (Exception ex)
                {
                    theTransaction.Dispose();

                    //CommonServicesProvider.LoggingService.AuditError(new UserPasswordResetEvent(model.SubjectUserId, Models.Enums.EventType.Error)
                    //                                                    , exception: ex);

                    throw;
                }
            }

            // passwordChanged log
            //await new UserPasswordChangeHistoryService(CommonServicesProvider).InsertAsync(subjectUserId, subjectUserName, newPassword, UserPasswordChangeType.ResetByRecovery);

            // auditing
            //CommonServicesProvider.LoggingService.AuditInfo(new UserPasswordResetEvent(model.SubjectUserId, Models.Enums.EventType.Success));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task ChangePasswordAsync(ChangePasswordDto model)
        {
            int subjectUserId;
            string subjectUserName;
            string newPassword;

            using (var theTransaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var foundUserEntity = await FindEntityAndThrowExceptionIfNotFoundAsync(CommonServicesProvider.LoggedInUserId.Value);
                    subjectUserId = foundUserEntity.Id;
                    subjectUserName = foundUserEntity.UserName;
                    newPassword = model.NewPassword;

                    validateOnChangePassword(model);

                    var changePasswordResult = await _appUserManager.ChangePasswordAsync(foundUserEntity, model.CurrentPassword, model.NewPassword);
                    if (!changePasswordResult.Succeeded)
                    {
                        throw new GeneralBusinessLogicException(changePasswordResult.GetAllErrors());
                    }

                    await _appUserManager.RefreshUpdateTimeAsync(foundUserEntity);

                    theTransaction.Complete();
                }
                catch (BaseBusinessException bex)
                {
                    theTransaction.Dispose();

                    //CommonServicesProvider.LoggingService.AuditError(new UserPasswordChangeEvent(model.Id, Models.Enums.EventType.Error), exception: bex);

                    throw;
                }
                catch (Exception ex)
                {
                    theTransaction.Dispose();

                    //CommonServicesProvider.LoggingService.AuditError(new UserPasswordChangeEvent(model.Id, Models.Enums.EventType.Error), exception: ex);

                    throw;
                }
            }

            // auditing
            //CommonServicesProvider.LoggingService.AuditInfo(new UserPasswordChangeEvent(model.Id, Models.Enums.EventType.Success));
        }

        private void validateOnChangePassword(ChangePasswordDto model)
        {
            model.ThrowIfNull();

            if (model.CurrentPassword.IsEmpty())
            {
                throw new BusinessValidationException("Current password cannot be empty");
            }

            if (model.NewPassword.IsEmpty())
            {
                throw new BusinessValidationException("New password cannot be empty");
            }

            if (model.NewPassword != model.NewPasswordRepeat)
            {
                throw new BusinessValidationException("New password and new password repeat must be the same");
            }
        }

        private void validateOnResetPassword(ResetPasswordDto model)
        {
            model.ThrowIfNull();

            if (model.NewPassword.IsEmpty())
            {
                throw new BusinessValidationException("New password cannot be empty");
            }
        }
    }
}
