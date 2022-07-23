using FinLib.DomainClasses.SEC;
using FinLib.Providers.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FinLib.Providers.Identity
{
    public class AppSignInManager : SignInManager<User>
    {
        private readonly UserManager<User> _userManager;
        private readonly IAppLogger _appLogger;

        public AppSignInManager(UserManager<User> userManager,
                                    IHttpContextAccessor contextAccessor,
                                    IUserClaimsPrincipalFactory<User> claimsFactory,
                                    IOptions<IdentityOptions> optionsAccessor,
                                    ILogger<SignInManager<User>> logger,
                                    IAuthenticationSchemeProvider authenticationSchemeProvider,
                                    IUserConfirmation<User> userConfirmation,
                                    IAppLogger appLogger)
            : base(userManager,
                  contextAccessor,
                  claimsFactory,
                  optionsAccessor,
                  logger,
                  authenticationSchemeProvider,
                  userConfirmation)
        {
            this._userManager = userManager;
            _appLogger = appLogger;
        }

        protected override async Task<SignInResult> LockedOut(User user)
        {
            var message = $"the Account has been locked because of too many incorrect login attempts, and will be unlock at {user.LockoutEnd.Value.LocalDateTime}";
            _appLogger.Info( Models.Enums.EventCategory.UserAccountManagement
                , Models.Enums.EventId.UserLockoutByWrongPasswordAttempts
                , Models.Enums.EventType.Success
                , message);

            user.LockoutDescription = message;
            await _userManager.UpdateAsync(user);

            return await base.LockedOut(user);
        }
    }
}
