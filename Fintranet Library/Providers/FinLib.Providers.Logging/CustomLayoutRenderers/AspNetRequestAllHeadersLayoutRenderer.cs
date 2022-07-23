using System.Text;
using NLog;
using NLog.LayoutRenderers;
using NLog.Web.LayoutRenderers;
using FinLib.Common.Extensions;

namespace FinLib.Providers.Logging.CustomLayoutRenderers
{
    /// <summary>
    /// Render all headers for ASP.NET Core
    /// </summary>
    /// <example>
    /// <code lang="NLog Layout Renderer">
    /// ${aspnet-request-all-headers}
    /// </code>
    /// </example>
    [LayoutRenderer("aspnet-request-all-headers")]
    public class AspNetRequestAllHeadersLayoutRenderer : AspNetLayoutRendererBase
    {
        protected override void DoAppend(StringBuilder builder, LogEventInfo logEvent)
        {
            var httpRequest = HttpContextAccessor?.HttpContext?.Request;
            if (httpRequest is null || httpRequest.Headers is null)
            {
                return;
            }

            var allHeadersJsoned = httpRequest.Headers.ToJson();

            builder.Append(allHeadersJsoned);
        }
    }
}
