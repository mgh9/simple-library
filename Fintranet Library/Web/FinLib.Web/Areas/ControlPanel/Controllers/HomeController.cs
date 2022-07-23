using FinLib.Models.Configs;
using FinLib.Providers.Identity;
using FinLib.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace FinLib.Web.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel"), Route("ControlPanel")]
    public class HomeController : Controller
    {
        private readonly AppUserManager _userManager;
        private readonly ICommonServicesProvider<GlobalSettings> _commonServicesProvider;

        public HomeController(AppUserManager userManager,ICommonServicesProvider<FinLib.Models.Configs.GlobalSettings> commonServicesProvider)
        {
            _userManager = userManager;
            _commonServicesProvider = commonServicesProvider;
        }

        public IActionResult IndexAsync()
        {
            return View();
        }
    }
}
