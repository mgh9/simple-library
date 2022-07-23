using FinLib.Common.Extensions;
using FinLib.Models.Configs;
using FinLib.Services.Base;
using FinLib.Web.Api.Helpers;
using FinLib.Web.Shared.Attributes;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace FinLib.Web.Api.Base
{
    [ApiController]
    [Route("api/[controller]/[action]/{id?}")]
    [MyAuthorize]
    public abstract partial class BaseController : Controller
    {
        protected BaseController(ICommonServicesProvider<GlobalSettings> commonServicesProvider)
        {
            CommonServicesProvider = commonServicesProvider;
            HttpContextAccessor = (IHttpContextAccessor)commonServicesProvider.ServiceProvider.GetService(typeof(IHttpContextAccessor));

            DataProtectionProviderKey = this.GetType().FullName;
            DataProtector = commonServicesProvider.DataProtectionProvider.CreateProtector(DataProtectionProviderKey);
        }

        public bool IsAuthenticated
        {
            get
            {
                return HttpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            }
        }

        protected IServiceProvider ServiceProvider { get; }
        protected ICommonServicesProvider<GlobalSettings> CommonServicesProvider { get; }
        protected IHttpContextAccessor HttpContextAccessor { get; }

        protected IDataProtector DataProtector { get; }
        internal string DataProtectionProviderKey { get; }

        protected int GetLoggedInUserId()
        {
            return HttpContextAccessor.HttpContext.User.GetLoggedInUserId<int>();
        }

        protected string GetLoggedInUserName()
        {
            return HttpContextAccessor.HttpContext.User.GetLoggedInUserName();
        }

        protected string GetLoggedInUserEmail()
        {
            return HttpContextAccessor.HttpContext.User.GetLoggedInUserEmail();
        }

        protected async Task<int> GetLoggedInUserRoleIdAsync()
        {
            return await UserManagerHelper.GetLoggedInUserRoleIdAsync(HttpContextAccessor, true);
        }
    }
}
