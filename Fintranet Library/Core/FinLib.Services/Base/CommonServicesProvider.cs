using FinLib.DataLayer.Context;
using FinLib.DomainClasses.SEC;
using FinLib.Mappings;
using FinLib.Models.Dtos.SEC;
using FinLib.Models.Enums;
using FinLib.Providers.Configuration;
using FinLib.Providers.Logging;
using FinLib.Services.SEC;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;

namespace FinLib.Services.Base
{
    public interface ICommonServicesProvider<TAppSettings>
        where TAppSettings : class, new()
    {
        IServiceProvider ServiceProvider { get; }
        IHttpContextAccessor HttpContextAccessor { get; }
        HttpContext CurrentHttpContext { get; }
        IAppLogger AppLogger { get; }
        IUnitOfWork DbContext { get; }
        IDataProtectionProvider DataProtectionProvider { get; }
        TAppSettings AppSettingsProvider { get; }
        IDistributedCache DistributedCacheProvider { get; }

        bool IsAuthenticated { get; }

        int? LoggedInUserId { get; }
        string LoggedInUserName { get; }
        int? LoggedInUserRoleId { get; }
        RoleDto LoggedInRole { get; }
        int? LoggedInRoleId { get; }
        string LoggedInRoleKey { get; }
        ApplicationRole? LoggedInRoleName { get; }
    }

    public class CommonServicesProvider<TAppSettings>: ICommonServicesProvider<TAppSettings>
        where TAppSettings : class, new()
    {
        public IServiceProvider ServiceProvider { get; }
        
        public IHttpContextAccessor HttpContextAccessor { get; }
        public HttpContext CurrentHttpContext { get; }
        
        public IAppLogger AppLogger { get; }
        
        public IUnitOfWork DbContext { get; }
        
        public IDataProtectionProvider DataProtectionProvider { get; }
        
        public TAppSettings AppSettingsProvider { get; }
        public IDistributedCache DistributedCacheProvider { get; }

        public bool IsAuthenticated { get; }
        public int? LoggedInUserId { get; private set; }
        public string LoggedInUserName { get; private set; }
        public int? LoggedInUserRoleId { get; private set; }
        public RoleDto LoggedInRole { get; private set; }
        public int? LoggedInRoleId { get; private set; }
        public string LoggedInRoleKey { get; private set; }
        public ApplicationRole? LoggedInRoleName { get; private set; }
        //public UserInfoJson LoggedInUserInfo { get; }

        public CommonServicesProvider(IServiceProvider serviceProvider
                                        , IHttpContextAccessor httpContextAccessor
                                        , IAppLogger appLogger
                                        , IUnitOfWork dbContext
                                        , IDataProtectionProvider dataProtectionProvider
                                        , IAppSettingsProvider<TAppSettings> appSettingsProvider
                                        , IDistributedCache distributedCacheProvider
                                        , bool isAuthenticated
                                        , int? loggedInUserId = null
                                        , int? loggedInUserRoleId = null)
        {
            ServiceProvider = serviceProvider;
            HttpContextAccessor = httpContextAccessor;
            CurrentHttpContext = httpContextAccessor?.HttpContext;
            AppLogger = appLogger;
            DbContext = dbContext;
            DataProtectionProvider = dataProtectionProvider;
            AppSettingsProvider = appSettingsProvider.Settings;
            DistributedCacheProvider = distributedCacheProvider;
            //UserManager = userManager;
            IsAuthenticated = isAuthenticated;

            if (IsAuthenticated)
            {
                LoggedInUserId = loggedInUserId;
                LoggedInUserRoleId = loggedInUserRoleId;

                fillLoggedInUserInfo();
            }
        }

        private void fillLoggedInUserInfo()
        {
            var query = from theUser in DbContext.Set<User>()

                        join theUserRole in DbContext.Set<UserRole>() on theUser.Id equals theUserRole.UserId
                        join theRole in DbContext.Set<Role>() on theUserRole.RoleId equals theRole.Id

                        where theUser.Id == LoggedInUserId
                            && theUserRole.Id == LoggedInUserRoleId
                        select new
                        {
                            UserInfo = theUser,
                            DefaultRoleInfo = theRole
                        };

            var theLoggedInUserAndRoleInfo = query.Single();

            LoggedInRole = theLoggedInUserAndRoleInfo.DefaultRoleInfo.MapTo<RoleDto>();
            LoggedInRoleId = LoggedInRole.Id;
            LoggedInRoleName = RoleService.GetRole(LoggedInRole.Name);
            LoggedInUserName = theLoggedInUserAndRoleInfo.UserInfo.UserName;
            //LoggedInUserInfo = theUserService.GetUserInfoByUserRoleId(LoggedInUserRoleId.Value);
        }
    }
}
