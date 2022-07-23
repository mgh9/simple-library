using Microsoft.AspNetCore.Mvc;

namespace FinLib.Web.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel"), Route("ControlPanel")]
    public class HomeController : Controller
    {
        public IActionResult IndexAsync()
        {
            return View();
        }
    }
}
