using FinLib.Common.Exceptions.Business;
using FinLib.Common.Exceptions.Business.User;
using FinLib.Common.Exceptions.Infra;
using FinLib.Common.Extensions;
using FinLib.DataLayer.Context;
using FinLib.DomainClasses.SEC;
using FinLib.Mappings;
using FinLib.Models.Base;
using FinLib.Models.Base.SearchFilters;
using FinLib.Models.Dto.SEC;
using FinLib.Models.Dtos;
using FinLib.Models.Dtos.SEC;
using FinLib.Models.Enums;
using FinLib.Models.SearchFilters.SEC;
using FinLib.Models.Views.SEC;
using FinLib.Providers.Identity;
using FinLib.Services.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinLib.Services.SEC
{
    public partial class UserService : UpdatableEntityService<FinLib.DomainClasses.SEC.User,
        FinLib.Models.Dtos.SEC.UserDto
        , FinLib.Models.Views.SEC.UserView, UserSearchFilter>
    {
        private readonly AppUserManager _appUserManager;

        private IQueryable<User> _activeUsers { get { return _repository.Where(x => x.IsActive); } }
        private IQueryable<User> _allUsers { get { return _repository; } }

        public UserService(ICommonServicesProvider<FinLib.Models.Configs.GlobalSettings> commonServicesProvider
            , AppUserManager appUserManager)
            : base(commonServicesProvider)
        {
            _appUserManager = appUserManager;
        }
    }

    public partial class UserService
    {
        public override async Task<List<UserDto>> GetAsync()
        {
            return _allUsers.MapTo<List<UserDto>>();
        }

        public override async Task<List<UserDto>> GetAsync(Expression<Func<User, bool>> where)
        {
            return _allUsers.Where(where).MapTo<List<UserDto>>();
        }

        public override async Task<GetResultDto<UserView>> GetAsync(GetRequestDto<UserSearchFilter> model)
        {
            var now = DateTime.Now;

            var query = from theUser in _allUsers

                        select new UserView
                        {
                            Id = theUser.Id,
                            FirstName = theUser.FirstName,
                            LastName = theUser.LastName,
                            FullName = theUser.FirstName + " " + theUser.LastName,
                            BirthDate = theUser.BirthDate,
                            Gender  = theUser.Gender,
                            UserName = theUser.UserName,
                            Email = theUser.Email,
                            Mobile = theUser.Mobile,
                            LastLoggedInTime = theUser.LastLoggedInTime,
                            IsActive = theUser.IsActive,
                            UpdateDate = theUser.UpdateDate,
                        };

            query = FilterService.ParseFilter(query, model.SearchFilterModel);
            int count = await query.CountAsync();

            var retval = new GetResultDto<UserView>(query
                    .OrderBy(model.PageOrder)
                    .Skip(model.PageIndex * model.PageSize)
                    .Take(model.PageSize)
                    .ToList(), count);

            // auditing
            CommonServicesProvider.AppLogger.Info(Models.Enums.EventCategory.EntityManagement
                , Models.Enums.EventId.EntityReadList,
                 Models.Enums.EventType.Success,
                _entityName, _entityTitle, null, null);

            return retval;
        }

        internal override async Task<User> GetEntityByIdAsync(int id)
        {
            return await _allUsers.SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<UserDto> GetByIdAsync(int id)
        {
            var foundUser = await _appUserManager.FindByIdAsync(id.ToString());
            if (foundUser == null)
                return null;

            var userInfo = foundUser.MapTo<UserDto>();
            userInfo.UserRoles = await GetHisRolesAsync(id);

            // auditing
            CommonServicesProvider.AppLogger.Info(Models.Enums.EventCategory.EntityManagement
                , Models.Enums.EventId.EntityRead, Models.Enums.EventType.Success
                , _entityName, _entityTitle, userInfo.ToJson(), null);

            return userInfo;
        }

        public async Task<List<TitleValue<int>>> GetCustomersUserRoleIdsTitleValueListAsync()
        {
            var customerRole = ApplicationRole.Customer.ToString().ToUpperInvariant();
            var query = from theUser in _allUsers
                        join theUserRole in DbContext.Set<UserRole>() on theUser.Id equals theUserRole.UserId
                        join theRole in DbContext.Set<Role>() on theUserRole.RoleId equals theRole.Id
                        where theRole.NormalizedName == customerRole
                        select new TitleValue<int>()
                        {
                            Title = theUser.FirstName + " " + theUser.LastName,
                            Value = theUserRole.Id
                        };

            var result = await query.OrderBy(item => item.Title).ToListAsync();

            return result;
        }

        public override async Task<List<TitleValue<int>>> GetTitleValueListAsync(bool includeEmptySelector = false, bool includeSelectAllSelector = false, string text = null)
        {
            var query = from theUser in _allUsers
                        join theUserRole in DbContext.Set<UserRole>() on theUser.Id equals theUserRole.UserId
                        join theRole in DbContext.Set<Role>() on theUserRole.RoleId equals theRole.Id
                        select new TitleValue<int>()
                        {
                            Title = theUser.FirstName + " " + theUser.LastName,
                            Value = theUserRole.Id
                        };

            if (!text.IsNotEmpty())
            {
                query = query.Where(item => item.Title.Contains(text));
            }

            var result = await query.OrderBy(item => item.Title).ToListAsync();

            if (includeSelectAllSelector)
            {
                result.Insert(0, new TitleValue<int> { Value = -1, Title = "{All}", Description = "Sellect all" });
            }

            if (includeEmptySelector)
            {
                result.Insert(0, new TitleValue<int> { Value = -1, Title = "{Empty}", Description = "----" });
            }

            return result;
        }

        public async Task<UserDto> GetByMobileAsync(string mobile)
        {
            if (mobile.IsEmpty())
                return null;

            var theEntity = _allUsers.SingleOrDefault(item => item.Mobile == mobile);
            if (theEntity is null)
                return null;

            var result = MapperHelper.MapTo<UserDto>(theEntity);
            result.UserRoles = await GetHisRolesAsync(theEntity.Id);

            // auditing
            CommonServicesProvider.AppLogger.Info( Models.Enums.EventCategory.EntityManagement, Models.Enums.EventId.EntityRead
                , Models.Enums.EventType.Success, _entityName , _entityTitle, theEntity.ToJson(),null);

            return result;
        }

        /// <summary>
        /// لیست تمام نقش های کاربر را میگرداند
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<List<UserRoleDto>> GetHisRolesAsync(int userId)
        {
            return await GetUserRolesInfoAsync(userId, false);
        }

        public async Task<RoleDto> GetRoleOfLoggedInUser()
        {
            if (!CommonServicesProvider.IsAuthenticated)
                throw new LogoutException("You were logged out");

            var hisRoleIdIfAny = (await new UserRoleService(CommonServicesProvider)
                                    .GetAsync(x => x.Id == CommonServicesProvider.LoggedInUserRoleId))
                                    .Select(x => x.RoleId);

            var hisRoleId = -1;
            if (hisRoleIdIfAny is null)
            {
                throw new InvalidUserRoleException(CommonServicesProvider.LoggedInUserId.Value, "The default userRole not found!");
            }
            else
            {
                hisRoleId = hisRoleIdIfAny.SingleOrDefault();
            }

            return await new RoleService(CommonServicesProvider).GetByIdAsync(hisRoleId);
        }

        public async Task<UserInfoDto> GetUserInfoByUserRoleIdAsync(int userRoleId, bool onlyActiveRoles)
        {
            UserInfoDto userInfo = null;

            var query = from theUser in _allUsers
                        join theUserRole in DbContext.Set<UserRole>() on theUser.Id equals theUserRole.UserId
                        where theUserRole.Id == userRoleId
                        select new UserInfoDto()
                        {
                            Id = theUser.Id,
                            UserName = theUser.UserName,
                            FirstName = theUser.FirstName,
                            LastName = theUser.LastName,
                            Email = theUser.Email,
                            ImageUrl = theUser.ImageUrl,
                            Mobile = theUser.Mobile,
                        };

            try
            {
                userInfo = await query.FirstAsync();
                userInfo.UserRoles = (await GetUserRolesInfoAsync(userInfo.Id, onlyActiveRoles));

                return userInfo;
            }
            catch (InvalidOperationException ex)
            {
                // auditing
                //CommonServicesProvider.LoggingService.AuditError(new UserReadEvent(userInfo?.Id ?? -1
                //                                                                    , _entityName
                //                                                                    , _entityTitle
                //                                                                    , userInfo.ToJson()
                //                                                                    , Models.Enums.EventType.Error)
                //                                                            , exception: ex
                //                                                            , customData: new { userRoleId, onlyActiveRoles }.ToJson());

                throw;
            }
        }

        protected override async Task<User> FindEntityAndThrowExceptionIfNotFoundAsync(int entityId)
        {
            try
            {
                var theEntity = await _appUserManager.FindByIdAsync(entityId);
                if (theEntity == null)
                {
                    throw new EntityNotFoundException(entityId);
                }

                return theEntity;
            }
            catch (EntityNotFoundException kex)
            {
                //CommonServicesProvider.LoggingService.AuditError(new EntityReadEvent(_entityName, _entityTitle, null, Models.Enums.EventType.Failure)
                //                                                    , exception: kex, customData: new { entityId }.ToJson());

                throw;
            }
            catch (Exception ex)
            {
                //CommonServicesProvider.LoggingService.AuditError(new EntityReadEvent(_entityName, _entityTitle, null, Models.Enums.EventType.Error)
                //                                                    , customData: new { entityId }.ToJson());

                throw new EntityNotFoundException(entityId, "An error occurred when searching for the user", ex);
            }
        }

        public async Task<UserInfoDto> GetUserInfoAsync(int userId, bool onlyActiveRoles)
        {
            UserInfoDto userInfo = null;

            var query = from theUser in _allUsers
                        where theUser.Id == userId
                        select new UserInfoDto()
                        {
                            Id = theUser.Id,
                            UserName = theUser.UserName,
                            FirstName = theUser.FirstName,
                            LastName = theUser.LastName,
                            Email = theUser.Email,
                            Mobile = theUser.Mobile,
                        };
            try
            {
                userInfo = await query.SingleAsync();
                userInfo.UserRoles = await GetUserRolesInfoAsync(userId, onlyActiveRoles);

                //CommonServicesProvider.LoggingService.AuditInfo(new UserReadEvent(userId, _entityName, _entityTitle, userInfo.ToJson(), Models.Enums.EventType.Information));

                return userInfo;
            }
            catch (InvalidOperationException ex)
            {
                //CommonServicesProvider.LoggingService.AuditError(new UserReadEvent(userId, _entityName, _entityTitle, userInfo.ToJson(), Models.Enums.EventType.Error)
                //                                                    , exception: ex);

                throw;
            }
        }

        public async Task<UserInfoDto> GetUserInfoAsync(string userName, bool onlyActiveRoles)
        {
            UserInfoDto userInfo = null;

            var normalizedUserName = userName.ToUpperInvariant();
            var query = from theUser in _allUsers
                        where theUser.NormalizedUserName == normalizedUserName
                        select new UserInfoDto()
                        {
                            Id = theUser.Id,
                            UserName = theUser.UserName,
                            FirstName = theUser.FirstName,
                            LastName = theUser.LastName,
                            Email = theUser.Email,
                            Mobile = theUser.Mobile,
                        };
            try
            {
                userInfo = await query.SingleAsync();
                userInfo.UserRoles = await GetUserRolesInfoAsync(userInfo.Id, onlyActiveRoles);

                //CommonServicesProvider.LoggingService.AuditInfo(new UserReadEvent(userInfo.Id, _entityName, _entityTitle, userInfo.ToJson(), Models.Enums.EventType.Information));

                return userInfo;
            }
            catch (InvalidOperationException ex)
            {
                //CommonServicesProvider.LoggingService.AuditError(new UserReadEvent(userName, _entityName, _entityTitle, userInfo.ToJson(), Models.Enums.EventType.Error)
                //                                                    , exception: ex);

                throw;
            }
        }

        public async Task<string> GetLoggedInUserRoleKey()
        {
            return (await GetRoleOfLoggedInUser()).Name;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1643:Strings should not be concatenated using '+' in a loop", Justification = "<Pending>")]
        public static async Task<List<UserRoleDto>> GetUserRolesInfoAsync(IUnitOfWork dbContext, int userId, bool onlyActiveRoles, bool renameDisableRoles = true)
        {
            var query = from theUserRole in dbContext.Set<UserRole>()
                        join theRole in dbContext.Set<Role>() on theUserRole.RoleId equals theRole.Id
                        where theUserRole.UserId == userId
                            && (!onlyActiveRoles || onlyActiveRoles && theUserRole.IsActive)

                        select new UserRoleDto
                        {
                            Id = theUserRole.Id,
                            UserId = theUserRole.UserId,
                            RoleId = theRole.Id,
                            RoleName = theRole.Name,
                            RoleTitle = theRole.Title,
                            IsActive = theUserRole.IsActive,
                            IsDefault = theUserRole.IsDefault,
                        };

            var listOfHisUserRoles = await query.ToListAsync();

            if (listOfHisUserRoles.Count == 0)
            {
                throw new InvalidUserRoleException(userId, $"The user doesn't have any active roles");
            }

            if (renameDisableRoles)
            {
                foreach (var theRole in listOfHisUserRoles)
                {
                    if (!theRole.IsActive)
                    {
                        theRole.RoleTitle += " (inactive)";
                    }
                }
            }

            return listOfHisUserRoles;
        }

        public async Task<List<UserRoleDto>> GetUserRolesInfoAsync(int userId, bool onlyActiveRoles)
        {
            return await GetUserRolesInfoAsync(DbContext, userId, onlyActiveRoles, true);
        }

        public override async Task<UserView> GetAsViewByIdAsync(int id)
        {
            var theUserProfileService = new UserProfileService(CommonServicesProvider, this);
            var hisProfile = await theUserProfileService.GetProfileAsync(id);

            var retval = hisProfile.MapTo<UserView>();

            var queryGetExtraInfo = from theUser in _repository
                                    where theUser.Id == id
                                    select new
                                    {
                                        UserInfo = theUser,
                                    };

            var userWithClaim = queryGetExtraInfo.First();
            retval.UpdateDate = userWithClaim.UserInfo.UpdateDate ?? userWithClaim.UserInfo.CreateDate;

            // auditing
            //CommonServicesProvider.LoggingService.AuditInfo(new UserReadEvent(id
            //                                                                    , _entityName
            //                                                                    , _entityTitle
            //                                                                    , retval.ToJson()
            //                                                                    , Models.Enums.EventType.Information));

            return retval;
        }

        protected virtual void ValidateOnGetModel<TSearchFilter>(GetRequestDto<TSearchFilter> model)
                            where TSearchFilter : BaseEntitySearchFilter, new()
        {
            model.ThrowIfNull();
        }

        protected virtual void ValidateOnGetByUserRoleIdModel<TSearchFilter>(GetByUserRoleIdRequestDto<TSearchFilter> model)
            where TSearchFilter : BaseEntitySearchFilter, new()
        {
            ValidateOnGetModel(model);

            if (model.UserRoleId <= 0)
                throw new FatalException("Invalid UserRoleID");
        }
    }
}
