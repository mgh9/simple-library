using FinLib.Common.Exceptions.Business;
using FinLib.Common.Extensions;
using FinLib.Common.Validators;
using FinLib.DomainClasses.SEC;
using FinLib.Mappings;
using FinLib.Models.Dtos.SEC;
using FinLib.Models.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace FinLib.Services.SEC
{
    public partial class UserService
    {
        protected override void ValidateOnInsert(UserDto model)
        {
            model.ThrowIfNull();

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

            if (string.IsNullOrWhiteSpace(model.Password))
                throw new BusinessValidationException("Password cannot be empty");

            if (model.Password.Length < CommonServicesProvider.AppSettingsProvider.Identity.PasswordPolicy.RequiredLength || model.Password.Length > CommonServicesProvider.AppSettingsProvider.Identity.PasswordPolicy.MaxLength)
                throw new BusinessValidationException($"The password length must be between {CommonServicesProvider.AppSettingsProvider.Identity.PasswordPolicy.RequiredLength} and {CommonServicesProvider.AppSettingsProvider.Identity.PasswordPolicy.MaxLength}");
        }

        public async Task<int> CreateWithDefaultRolesAsync(UserDto model)
        {
            model.UserRoles = new List<UserRoleDto>
            {
                new UserRoleDto { IsActive = true, IsDefault = true, RoleId = (int)ApplicationRole.Customer}
            };

            return await InsertAsync(model);
        }

        public override async Task<int> InsertAsync(UserDto model)
        {
            User aUserToCreate = null;

            using (var theTransaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    ValidateOnInsert(model);
                    aUserToCreate = prepareUserToCreate(model);

                    var result = await _appUserManager.CreateAsync(aUserToCreate, model.Password);
                    if (!result.Succeeded)
                    {
                        throw new EntityCreateException(result.GetAllErrors());
                    }

                    if (model.UserRoles != null)
                    {
                        var theUserRoleService = new UserRoleService(CommonServicesProvider);
                        foreach (var theRoleItem in model.UserRoles)
                        {
                            var newUserRole = new UserRoleDto
                            {
                                UserId = aUserToCreate.Id,
                                RoleId = theRoleItem.RoleId,
                                IsActive = theRoleItem.IsActive,
                            };

                            await theUserRoleService.InsertAsync(newUserRole);
                        }
                    }

                    theTransaction.Complete();
                }
                catch (DbUpdateException dbex)
                {
                    theTransaction.Dispose();

                    if ((dbex.InnerException is SqlException theSqlException) && theSqlException.Number == 2601)
                    {
                        var errorMessage = theSqlException.Message;

                        if (theSqlException.Message.Contains("IX_Users_Email"))
                        {
                            errorMessage = "Duplicate email";
                        }
                        else if (theSqlException.Message.Contains("IX_Users_UserName"))
                        {
                            errorMessage = "duplicate username";
                        }
                        else if (theSqlException.Message.Contains("IX_Users_MobileNumber"))
                        {
                            errorMessage = "duplicate mobile";
                        }

                        // auditing
                        //CommonServicesProvider.LoggingService.AuditError(new UserCreateEvent(model.UserName
                        //                                                                    , _entityName
                        //                                                                    , _entityTitle
                        //                                                                    , model.ToJson()
                        //                                                                    , Models.Enums.EventType.Failure
                        //                                                                    , message: errorMessage)
                        //                                                    , exception: dbex);

                        throw new EntityCreateException(errorMessage, dbex);
                    }
                    else
                    {
                        // auditing
                        //CommonServicesProvider.LoggingService.AuditError(new UserCreateEvent(model.UserName
                        //                                                                    , _entityName
                        //                                                                    , _entityTitle
                        //                                                                    , model.ToJson()
                        //                                                                    , Models.Enums.EventType.Error)
                        //                                                    , exception: dbex);

                        throw new EntityCreateException("an error occured inserting a user", dbex);
                    }
                }
                catch (Exception ex)
                {
                    theTransaction.Dispose();

                    // auditing
                    //CommonServicesProvider.LoggingService.AuditError(new UserCreateEvent(model.UserName
                    //                                                                    , _entityName
                    //                                                                    , _entityTitle
                    //                                                                    , model.ToJson()
                    //                                                                    , Models.Enums.EventType.Error)
                    //                                                , exception: ex);

                    throw;
                }
            }

            //// auditing
            //CommonServicesProvider.LoggingService.AuditInfo(new UserCreateEvent(model.UserName
            //                                                                    , _entityName
            //                                                                    , _entityTitle
            //                                                                    , aUserToCreate.ToJson()
            //                                                                    , Models.Enums.EventType.Success));

            return aUserToCreate.Id;
        }

        private User prepareUserToCreate(UserDto model)
        {
            var aUserToCreate = model.MapTo<User>();

            aUserToCreate.FirstName = model.FirstName;
            aUserToCreate.LastName = model.LastName;
            aUserToCreate.UserName = model.UserName;
            aUserToCreate.Email = model.Email;
            aUserToCreate.Gender = model.Gender;
            aUserToCreate.ImageUrl = model.ImageUrl;

            aUserToCreate.Mobile = model.Mobile;

            aUserToCreate.IsActive = model.IsActive;
            aUserToCreate.CreateDate = DateTime.Now;

            return aUserToCreate;
        }

        //private User prepareUserToCreate(UserDto user, string password)
        //{
        //    var theUser = MapperHelper.MapTo<User>(user);

        //    //theUser.NormalizedUserName = theUser.UserName.ToUpperInvariant();
        //    //theUser.NormalizedEmail = theUser.Email?.ToUpperInvariant();
        //    //theMappedEntity.SecurityStamp = Guid.NewGuid().ToString("D");
        //    theUser.PasswordHash = _passwordHasher.HashPassword(theUser, password);

        //    return theUser;
        //}
    }
}
