using FinLib.Common.Exceptions.Business;
using FinLib.Common.Exceptions.Infra;
using FinLib.Common.Extensions;
using FinLib.Common.Validators;
using FinLib.DataLayer.Context;
using FinLib.DomainClasses.SEC;
using FinLib.Mappings;
using FinLib.Models.Constants.Database;
using FinLib.Models.Dtos.SEC;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace FinLib.Services.SEC
{
    public partial class UserService
    {
        //public async Task<IdentityResult> UpdateLastLogggedInTimeAsync(int userId)
        //{
        //    var theUserEntity = await _appUserManager.FindByIdAsync(userId.ToString());
        //    theUserEntity.LastLoggedInTime = DateTime.Now;
        //    var result = await _appUserManager.UpdateAsync(theUserEntity);

        //    return result;
        //}

        public override async Task DeleteAsync(int id)
        {
            User theUserEntity = null;

            try
            {
                theUserEntity = await FindEntityAndThrowExceptionIfNotFoundAsync(id);
                ValidateOnDelete(theUserEntity);

                var result = await _appUserManager.DeleteAsync(theUserEntity);
                if (!result.Succeeded)
                {
                    throw new EntityDeleteException(id, result.GetAllErrors());
                }

                // auditing
                CommonServicesProvider.AppLogger.Info(Models.Enums.EventCategory.UserAccountManagement
                    , Models.Enums.EventId.EntityDelete, Models.Enums.EventType.Success
                    , _entityName, _entityTitle, theUserEntity.ToJson(), null);
            }
            catch (DbUpdateException dbex)
            {
                if (dbex.GetBaseException() is SqlException sqlEx)
                {
                    var number = sqlEx.Number;
                    if (number == ErrorCodes.ForeignKeyReferencedByOthers)
                    // reference khorde az in User b jahaye dg va ghabele hazf nmibaashad
                    {
                        var errorMessage = "This user has performed operations in the system and cannot be deleted";

                        // auditing
                        CommonServicesProvider.AppLogger.Error(Models.Enums.EventCategory.UserAccountManagement
                            , Models.Enums.EventId.EntityDelete, Models.Enums.EventType.Error, _entityName
                                                                                            , _entityTitle
                                                                                            , theUserEntity.ToJson()
                                                                                            , null
                                                                                            , message: errorMessage
                                                                                            , exception: dbex);

                        throw new EntityDeleteException(id, errorMessage);
                    }
                }
                else
                {
                    // auditing
                    CommonServicesProvider.AppLogger.Error(Models.Enums.EventCategory.UserAccountManagement
                        , Models.Enums.EventId.EntityDelete, Models.Enums.EventType.Error, _entityName
                                                                                        , _entityTitle
                                                                                        , theUserEntity.ToJson()
                                                                                        , null
                                                                                        , message: dbex.Message
                                                                                        , exception: dbex);
                }



                throw;
            }
            catch (Exception ex)
            {
                // auditing
                CommonServicesProvider.AppLogger.Error(Models.Enums.EventCategory.UserAccountManagement
                    , Models.Enums.EventId.EntityDelete, Models.Enums.EventType.Error, _entityName
                                                                                    , _entityTitle
                                                                                    , theUserEntity.ToJson()
                                                                                    , null
                                                                                    , message: ex.Message
                                                                                    , exception: ex);

                throw;
            }
        }

        protected override void ValidateOnUpdate(UserDto model)
        {
            model.ThrowIfNull();

            // TODO: use FluentValidations

            if (model.Id <= 0)
            {
                throw new InvalidPrimaryKeyException(nameof(model.Id));
            }

            if (model.LastName.IsEmpty())
            {
                throw new BusinessValidationException("Lastname cannot be empty");
            }

            if (model.UserName.IsEmpty())
            {
                throw new BusinessValidationException("Username cannot be empty");
            }

            var usernameMinLen = CommonServicesProvider.AppSettingsProvider.Identity.UserNamePolicy.RequiredLength;
            var usernameMaxLen = CommonServicesProvider.AppSettingsProvider.Identity.UserNamePolicy.MaxLength;
            if (model.UserName.Length < usernameMinLen || model.UserName.Length > usernameMaxLen)
            {
                throw new BusinessValidationException($"The username length must be between {usernameMinLen} and {usernameMaxLen}");
            }

            if (!PersonValidator.IsValidEmail(model.Email))
            {
                throw new BusinessValidationException("Invalid email address");
            }

            bool isThereAnyActiveRoles = model.UserRoles.Any(item => item.IsActive);
            if (!isThereAnyActiveRoles)
            {
                throw new BusinessValidationException("The user must has 1 active role");
            }
        }

        protected override void PrepareModelOnUpdate(UserDto model)
        {
            base.PrepareModelOnUpdate(model);
        }

        public override async Task UpdateAsync(UserDto model)
        {
            User userEntityCloned = null;

            using (var theTransaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var theEntityToUpdate = FindEntityAndThrowExceptionIfNotFoundAsync(model.Id).Result;

                    ValidateOnUpdate(model);
                    PrepareModelOnUpdate(model);

                    userEntityCloned = theEntityToUpdate.Clone();

                    MapperHelper.Map(model, theEntityToUpdate);

                    theEntityToUpdate.UpdateDate = DateTime.Now;
                    theEntityToUpdate.UpdatedByUserRoleId = CommonServicesProvider.LoggedInUserRoleId;

                    // update the User himself
                    var result = await _appUserManager.UpdateAsync(theEntityToUpdate);
                    if (!result.Succeeded)
                    {
                        throw new EntityUpdateException(model.Id, result.GetAllErrors());
                    }

                    // update his roles
                    await updateUserRoles(model, theEntityToUpdate);

                    //
                    theTransaction.Complete();
                }
                catch (DbUpdateException dbex)
                {
                    theTransaction.Dispose();

                    if ((dbex.InnerException is SqlException theSqlException) && theSqlException.Number == 2601)
                    {
                        var errorMessage = "";

                        if (theSqlException.Message.Contains("IX_Users_Email"))
                        {
                            errorMessage = "Duplicate email";
                        }
                        else if (theSqlException.Message.Contains("IX_Users_UserName"))
                        {
                            errorMessage = "Duplicate username";
                        }
                        else if (theSqlException.Message.Contains("IX_Users_MobileNumber"))
                        {
                            errorMessage = "Duplicate mobile";
                        }
                        else
                        {
                            throw;
                        }

                        // auditing
                        CommonServicesProvider.AppLogger.Error(Models.Enums.EventCategory.Authentication
                            , Models.Enums.EventId.EntityUpdate, Models.Enums.EventType.Failure
                            , _entityName, _entityTitle, userEntityCloned.ToJson()
                            , model.ToJson(), message: errorMessage, exception: dbex);

                        throw new EntityUpdateException(model.Id, errorMessage, dbex);
                    }
                    else
                    {
                        // auditing
                        CommonServicesProvider.AppLogger.Error(Models.Enums.EventCategory.Authentication
                            , Models.Enums.EventId.EntityUpdate, Models.Enums.EventType.Failure
                            , _entityName, _entityTitle, userEntityCloned.ToJson()
                            , model.ToJson(), message: dbex.Message, exception: dbex);

                        throw new EntityUpdateException(model.Id, "Error in saving user info", dbex);
                    }
                }
                catch (Exception ex)
                {
                    theTransaction.Dispose();

                    // auditing
                    CommonServicesProvider.AppLogger.Error(Models.Enums.EventCategory.Authentication
                        , Models.Enums.EventId.EntityUpdate, Models.Enums.EventType.Failure
                        , _entityName, _entityTitle, userEntityCloned.ToJson()
                        , model.ToJson(), message: ex.Message, exception: ex);

                    throw;
                }
            }

            // auditing
            CommonServicesProvider.AppLogger.Info(Models.Enums.EventCategory.EntityManagement
                , Models.Enums.EventId.EntityUpdate, Models.Enums.EventType.Success,
                _entityName, _entityTitle, userEntityCloned.ToJson(), model.ToJson());
        }

        private async Task updateUserRoles(UserDto model, User userEntityToUpdate)
        {
            var theUserRoleService = new UserRoleService(CommonServicesProvider);
            var itsUserRoles = await theUserRoleService.GetTitleValueByUserIdAsync(userEntityToUpdate.Id);

            var toInsert = model.UserRoles.Where(r => !itsUserRoles.Any(c => c.Value == r.RoleId && r.Id != -1)).ToList();
            var toDelete = itsUserRoles.Where(r => !model.UserRoles.Any(c => c.RoleId == r.Value && c.Id != -1)).ToList();
            var toUpdate = model.UserRoles.Where(r => !toInsert.Any(c => c.RoleId == r.RoleId) && !toDelete.Any(c => c.Value == r.RoleId)).ToList();

            foreach (var theUserRoleItem in toDelete)
            {
                await theUserRoleService.DeleteByRoleIdAsync(userEntityToUpdate.Id, theUserRoleItem.Value);
            }

            foreach (var theUserRoleItem in toInsert)
            {
                await theUserRoleService.InsertAsync(new UserRoleDto
                {
                    UserId = userEntityToUpdate.Id,
                    RoleId = theUserRoleItem.RoleId,
                    IsActive = theUserRoleItem.IsActive,
                });
            }

            foreach (var theUserRoleItem in toUpdate)
            {
                var theUserRole = await theUserRoleService.GetByIdAsync(theUserRoleItem.Id);
                theUserRole.IsActive = theUserRoleItem.IsActive;
                await theUserRoleService.UpdateAsync(theUserRole);
            }
        }

        public async Task SaveUserRolesAsync(UserDto model)
        {
            var theUserRoleService = new UserRoleService(CommonServicesProvider);

            var hisCurrentRoles = theUserRoleService.GetByUserId(model.Id);

            var rolesToInsert = model.UserRoles.Where(x => !hisCurrentRoles.Any(y => y.RoleId == x.RoleId));
            var rolesToUpdate = model.UserRoles.Where(x => hisCurrentRoles.Any(y => y.Id == x.Id));
            var rolesToDelete = hisCurrentRoles.Where(x => !model.UserRoles.Any(y => y.RoleId == x.RoleId));

            try
            {
                await theUserRoleService.BeginTransactionAsync();

                foreach (var roleItemToInsert in rolesToInsert)
                {
                    var aUserRole = new UserRoleDto { UserId = model.Id, RoleId = roleItemToInsert.RoleId, IsActive = true };
                    await theUserRoleService.InsertAsync(aUserRole);
                }

                foreach (var roleItemToDelete in rolesToDelete)
                {
                    await theUserRoleService.DeleteAsync(roleItemToDelete.Id);
                }

                foreach (var roleItemToUpdate in rolesToUpdate)
                {
                    await theUserRoleService.UpdateAsync(roleItemToUpdate);
                }

                // update user's data update time
                await _appUserManager.RefreshUpdateTimeAsync(model.Id);

                var hisCurrentRolesAfterUpdate = theUserRoleService.GetByUserId(model.Id);
                if (!hisCurrentRolesAfterUpdate.Any(x => x.IsActive))
                {
                    throw new Common.Exceptions.Business.User.InvalidUserRoleException
                        (model.Id, "The user has to have a active role");
                }

                // auditing

                CommonServicesProvider.AppLogger.Info(Models.Enums.EventCategory.EntityManagement,
                     Models.Enums.EventId.EntityUpdate, Models.Enums.EventType.Success
                     , _entityName, _entityTitle, hisCurrentRoles.ToJson()
                     , hisCurrentRolesAfterUpdate.ToJson(), message: "UserRoles have been updated");

                await theUserRoleService.CommitAsync();
            }
            catch (Exception ex)
            {
                await theUserRoleService.RollbackAsync();

                // auditing
                CommonServicesProvider.AppLogger.Error(Models.Enums.EventCategory.UserAccountManagement
                    , Models.Enums.EventId.EntityUpdate, Models.Enums.EventType.Failure
                    , _entityName, _entityTitle, hisCurrentRoles.ToJson(), model.UserRoles.ToJson()
                    , exception: ex);

                throw;
            }
        }

        public static void SetDefaultUserRole(IUnitOfWork dbContext, int userId, int newDefaultUserRoleId)
        {
            if (newDefaultUserRoleId <= 0)
            {
                throw new InvalidPrimaryKeyException(nameof(newDefaultUserRoleId));
            }

            var userRoles = dbContext.Set<UserRole>().Where(item => item.UserId == userId).ToList();

            // set all of his roles not-default first
            foreach (var userRole in userRoles)
            {
                userRole.IsDefault = false;
            }

            // set the desired role as default
            UserRole userRoleToSetAsDefault = userRoles.SingleOrDefault(item => item.Id == newDefaultUserRoleId);
            if (userRoleToSetAsDefault == null)
            {
                throw new IllegalRequestException("Invalid userRole ID");
            }

            userRoleToSetAsDefault.IsDefault = true;
            dbContext.SaveChanges();
        }

        public void SetDefaultUserRole(int userId, int newDefaultUserRoleId)
        {
            SetDefaultUserRole(DbContext, userId, newDefaultUserRoleId);
        }
    }
}