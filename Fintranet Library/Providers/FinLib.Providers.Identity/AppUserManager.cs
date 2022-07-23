using FinLib.Common.Extensions;
using FinLib.DataLayer.Context;
using FinLib.DomainClasses.SEC;
using FinLib.Models.Enums;
using FinLib.Providers.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace FinLib.Providers.Identity
{
    public partial class AppUserManager : UserManager<User>
    {
        private readonly IUnitOfWork _dbContext;
        private readonly IAppLogger _appLogger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppUserManager(IUserStore<User> store,
                                IOptions<IdentityOptions> optionsAccessor,
                                IPasswordHasher<User> passwordHasher,
                                IEnumerable<IUserValidator<User>> userValidators,
                                IEnumerable<IPasswordValidator<User>> passwordValidators,
                                ILookupNormalizer keyNormalizer,
                                IdentityErrorDescriber errors,
                                IServiceProvider services,
                                ILogger<UserManager<User>> logger,
                                IUnitOfWork dbContext,
                                IAppLogger appLogger,
                                IHttpContextAccessor httpContextAccessor
                                )
            : base(store,
                      optionsAccessor,
                      passwordHasher,
                      userValidators,
                      passwordValidators,
                      keyNormalizer,
                      errors,
                      services,
                      logger)
        {
            _dbContext = dbContext;
            _appLogger = appLogger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async override Task<User> FindByIdAsync(string userId)
        {
            var data = await base.FindByIdAsync(userId);
            //if (!data.IsActive)
            //    return null;

            return data;
        }

        public override Task<User> GetUserAsync(ClaimsPrincipal principal)
        {
            return base.GetUserAsync(principal);
        }

        public async Task<User> FindByIdAsync(int userId)
        {
            return await FindByIdAsync(userId.ToString());
        }

        public async Task UpdateLastLoggedInTimeAsync(string userName)
        {
            var theUser = await FindByNameAsync(userName);
            await UpdateLastLoggedInTimeAsync(theUser);
        }

        public async Task UpdateLastLoggedInTimeAsync(User user)
        {
            user.LastLoggedInTime = DateTime.Now;
            await UpdateAsync(user);
        }

        /// <summary>
        /// فیلد زمان بروزرسانی اطلاعات کاربر رو بروز میکنه
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task RefreshUpdateTimeAsync(User user)
        {
            user.UpdateDate = DateTime.Now;
            await UpdateAsync(user);
        }

        public async Task RefreshUpdateTimeAsync(int userId)
        {
            var theUser = await FindByIdAsync(userId);
            theUser.UpdateDate = DateTime.Now;
            await UpdateAsync(theUser);
        }

        public async Task<bool> IsAdminAsync(User user)
        {
            return await IsInRoleAsync(user, ApplicationRole.Admin);
        }

        public override async Task<IList<string>> GetRolesAsync(User user)
        {
            var query = from theUserRole in _dbContext.Set<UserRole>()
                        join theUser in _dbContext.Set<User>() on theUserRole.UserId equals theUser.Id
                        join theRole in _dbContext.Set<Role>() on theUserRole.RoleId equals theRole.Id
                        where theUser.Id == user.Id
                            && theUserRole.IsActive
                        select theRole.Name;

            return await query.ToListAsync();
        }

        public override async Task<IList<User>> GetUsersInRoleAsync(string roleName)
        {
            var query = from theUserRole in _dbContext.Set<UserRole>()
                        join theUser in _dbContext.Set<User>() on theUserRole.UserId equals theUser.Id
                        join theRole in _dbContext.Set<Role>() on theUserRole.RoleId equals theRole.Id
                        where theRole.Name == roleName
                            && theUserRole.IsActive
                            && theUser.IsActive
                        select theUser;

            return await query.ToListAsync();
        }

        public override async Task<bool> IsInRoleAsync(User user, string role)
        {
            if (user == null)
                return false;

            var query = from theUserRole in _dbContext.Set<UserRole>()
                        join theUser in _dbContext.Set<User>() on theUserRole.UserId equals theUser.Id
                        join theRole in _dbContext.Set<Role>() on theUserRole.RoleId equals theRole.Id
                        where theUser.Id == user.Id
                            && theRole.Name == role
                            && theUserRole.IsActive
                        select theUserRole;

            return await query.AnyAsync();
        }

        public async Task<bool> IsInRoleAsync(User user, ApplicationRole role)
        {
            return await IsInRoleAsync(user, role.ToString());
        }

        public override async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            validateOnChangePassword(user, currentPassword, newPassword);

            var result = await base.ChangePasswordAsync(user, currentPassword, newPassword);

            return result;
        }

        private void validateOnChangePassword(User user, string currentPassword, string newPassword)
        {
            user.ThrowIfNull();
            currentPassword.ThrowIfNull();
            newPassword.ThrowIfNull();
        }

        public override async Task<IList<User>> GetUsersForClaimAsync(System.Security.Claims.Claim claim)
        {
            var data = await base.GetUsersForClaimAsync(claim);
            data = data.Where(x => x.IsActive).ToList();
            return data;
        }
        
        public override IQueryable<User> Users => base.Users.Where(x=> x.IsActive);

        public async Task<IdentityResult> AddToRoleAsync(User user, string role, bool isActive, bool isDefault)
        {
            var result = await AddToRoleAsync(user, role);
            if (result.Succeeded && isActive)
            {
                var query = from theUserRole in _dbContext.Set<UserRole>()
                            join theRole in _dbContext.Set<Role>() on theUserRole.RoleId equals theRole.Id
                            where theUserRole.UserId == user.Id
                                && theRole.Name == role
                            select theUserRole;
                var justCreatedUserRole = query.Single();

                justCreatedUserRole.IsActive = isActive;
                justCreatedUserRole.IsDefault = isDefault;

                _dbContext.Set<UserRole>().Update(justCreatedUserRole);
                await _dbContext.SaveChangesAsync();
            }

            return result;
        }

        public override Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            return base.AddToRoleAsync(user, role);
        }
    }
}
