using Microsoft.AspNetCore.Mvc;
using FinLib.Web.Shared.Attributes;

namespace FinLib.Web.Controllers.Base
{
    [MyAuthorize]
    [AutoValidateAntiforgeryToken]
    public abstract class BaseAuthorizedController: BaseController
    {

    }
}
