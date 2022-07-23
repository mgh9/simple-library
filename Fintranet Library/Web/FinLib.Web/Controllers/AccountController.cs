using FinLib.Common.Extensions;
using FinLib.Models.Configs;
using FinLib.Providers.Configuration;
using FinLib.Providers.Identity;
using FinLib.Providers.Logging;
using FinLib.Services.Base;
using FinLib.Services.SEC;
using FinLib.Web.Shared.Attributes;
using FinLib.Web.Shared.Models.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Wangkanai.Detection.Services;
using User = FinLib.DomainClasses.SEC.User;

namespace FinLib.Web.Controllers
{
    public partial class AccountController : Base.BaseAuthorizedController
    {
        private readonly GlobalSettings _globalSettings;
        private readonly AppUserManager _userManager;
        private readonly SignInManager<User> _signInManager;

        private readonly IAppLogger _appLogger;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly ICommonServicesProvider<GlobalSettings> _commonServicesProvider;

        private readonly UserService _userService;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public AccountController(IAppLogger appLogger,
                                    AppUserManager userManager,
                                    SignInManager<User> signInManager,

                                    UserService userService,
                                    IDetectionService detectionService,
                                    IAppSettingsProvider<GlobalSettings> globalSettingsProvider,
                                    IHostApplicationLifetime hostApplicationLifetime,
                                    ICommonServicesProvider<FinLib.Models.Configs.GlobalSettings> commonServicesProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appLogger = appLogger;
            _commonServicesProvider = commonServicesProvider;

            _hostApplicationLifetime = hostApplicationLifetime;
            _userService = userService;
            _globalSettings = globalSettingsProvider.Settings;
        }

        // GET: /Account/Login
        /// <summary>
        /// Entry point into the login workflow
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);

            //if (User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            //return View();
        }

        // POST: /Account/Login
        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [PreventDuplicateRequest]
        public async Task<IActionResult> LoginAsync(LoginViewModel model, string button)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Username,model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        await _userManager.UpdateLastLoggedInTimeAsync(model.Username);
                        if (model.ReturnUrl.IsNotEmpty() && Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                ModelState.AddModelError("", "Invalid login attempt");
                return View(model);
            }
            catch (Exception ex)
            {
                //_appLogger.AuditError(new UserLoginFailureEvent(model.UserName, "اشکالی در ورود حساب کاربری شما رخ داده است. با پشتیبانی سامانه تماس حاصل فرمایید", clientId: context?.Client.ClientId), ex);

                ModelState.AddModelError("", "اشکالی در ورود حساب کاربری شما رخ داده است. با پشتیبانی سامانه تماس حاصل فرمایید");
                return View(model);
            }
        }
 
        [HttpGet]
        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}