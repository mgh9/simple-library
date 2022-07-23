using FinLib.Common.Extensions;
using FinLib.Models.Configs;
using FinLib.Providers.Configuration;
using FinLib.Providers.Identity;
using FinLib.Providers.Logging;
using FinLib.Web.Shared.Attributes;
using FinLib.Web.Shared.Models.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using User = FinLib.DomainClasses.SEC.User;

namespace FinLib.Web.Controllers
{
    public partial class AccountController : Base.BaseAuthorizedController
    {
        private readonly AppUserManager _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAppLogger _appLogger;
        private readonly GlobalSettings _globalSettings;

        public AccountController(IAppLogger appLogger,
                                    AppUserManager userManager,
                                    SignInManager<User> signInManager,
                                    IAppSettingsProvider<GlobalSettings> globalSettingsProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appLogger = appLogger;
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
            catch 
            {
                //_appLogger.AuditError(.............);

                ModelState.AddModelError("", "There is an error in signing u in, please contact the support");
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