using FinLib.Models.Enums;
using Microsoft.AspNetCore.Authorization;

namespace FinLib.Web.Shared.Attributes
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        public MyAuthorizeAttribute()
            :base()
        {

        }

        public MyAuthorizeAttribute(params string[] roles)
            : base()
        {
            Roles = string.Join(",", roles);
        }


        public MyAuthorizeAttribute(params ApplicationRole[] roles)
            : base()
        {
            Roles = string.Join(",", roles);
        }

    }
}
