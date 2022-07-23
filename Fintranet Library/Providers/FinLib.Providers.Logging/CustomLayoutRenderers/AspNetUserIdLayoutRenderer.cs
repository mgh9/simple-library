using NLog;
using NLog.LayoutRenderers;
using NLog.Web.LayoutRenderers;
using System.Security.Claims;
using System.Text;

namespace FinLib.Providers.Logging.CustomLayoutRenderers
{
    [LayoutRenderer("aspnet-user-id")]
    public class AspNetUserIdLayoutRenderer : AspNetLayoutRendererBase
    {
        protected override void DoAppend(StringBuilder builder, LogEventInfo logEvent)
        {
            var context = HttpContextAccessor?.HttpContext;
            if (context?.User?.Identity is null)
            {
                return;
            }

            var loggedInUserId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            builder.Append(loggedInUserId);
        }
    }
}
