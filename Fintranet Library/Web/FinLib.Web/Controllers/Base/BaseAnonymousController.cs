using Microsoft.AspNetCore.Authorization;

namespace FinLib.Web.Controllers.Base
{
    [AllowAnonymous]
    public abstract class BaseAnonymousController: BaseController
    {

    }
}
