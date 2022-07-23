using FinLib.Admin.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace FinLib.Web.Extensions
{
    public static class ExceptionHandlingMidlewareExtensions
    {
        public static void ConfigureApiExceptionsHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
