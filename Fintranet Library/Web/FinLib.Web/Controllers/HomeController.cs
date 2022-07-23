using FinLib.Common.Extensions;
using FinLib.DomainClasses.SEC;
using FinLib.Models.Configs;
using FinLib.Providers.Configuration;
using FinLib.Providers.Identity;
using FinLib.Providers.Logging;
using FinLib.Web.Shared.Attributes;
using FinLib.Web.Shared.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace FinLib.Web.Controllers
{
    [MyAuthorize]
    public class HomeController : Base.BaseAuthorizedController
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IAppLogger _appLogger;
        private readonly GlobalSettings _globalSettings;
        private readonly AppUserManager _userManager;

        public HomeController(IWebHostEnvironment environment
            , IAppLogger appLogger
            , IAppSettingsProvider<GlobalSettings> globalSettingsProvider
            , AppUserManager userManager)
        {
            _environment = environment;
            _appLogger = appLogger;
            _globalSettings = globalSettingsProvider.Settings;
            _userManager = userManager;
        }

        public IActionResult Time()
        {
            return Content(DateTime.Now.ToString());
        }

        public IActionResult Index()
        {
            return Redirect("/ControlPanel");
        }

        private IActionResult handleRedirectToChangePassword(User authenticatedUser, string returnUrl)
        {
            return RedirectToAction("ChangePassword", "Account",
                new
                {
                    userName = authenticatedUser.UserName,
                    returnUrl = returnUrl
                });
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        /// <summary>
        /// Shows the error page
        /// </summary>
        public IActionResult Error(string errorId)
        {
            var vm = new GeneralErrorViewModel();
            vm.ContextTraceId = HttpContext.TraceIdentifier;

            //// retrieve error details from identityserver
            //var message = await _interaction.GetErrorContextAsync(errorId);
            //if (message != null)
            //{
            //    vm.CoreError = message;

            //    if (!_environment.IsDevelopment())
            //    {
            //        // only show in development
            //        message.ErrorDescription = null;
            //    }
            //}

            //var idpTraceId = _appLogger.AuditError(Models.Enums.AuditHelpers.Error.Category
            //                                            , Models.Enums.AuditHelpers.Error.UnhandledException
            //                                            , message: message?.ErrorDescription
            //                                            , customData: vm.ToJson());
            //vm.IdpTraceId = idpTraceId;

            return View("Error", vm);
        }

        //[HttpPost]//("~/cspreport")]
        //public IActionResult CspReport([FromBody] CspReportRequest request)
        //{
        //    // TODO: maybe log the request to our datastore..somewhere
        
        //    _loggingService.Trace($"CSP Violation: {request.CspReport.DocumentUri}, {request.CspReport.BlockedUri}");
        //    return Ok();
        //}
    }
}