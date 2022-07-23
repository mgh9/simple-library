using FinLib.Common.Exceptions.Base;
using FinLib.Common.Extensions;
using FinLib.Providers.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JsonResult = FinLib.Models.Base.JsonResult;

namespace FinLib.Admin.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private const string ContentType = "application/json";
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext httpContext, [FromServices] IAppLogger appLogger)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var traceId = appLogger.Error( Models.Enums.EventCategory.Application
                                                            , Models.Enums.EventId.UnhandledException
                                                            ,  Models.Enums.EventType.Error
                                                            , ex.Message
                                                            , ex);

                await handleOnExceptionAsync(httpContext, ex, traceId);
            }
        }

        private async Task handleOnExceptionAsync(HttpContext context, Exception exception, string idpTraceId)
        {
            context.Response.ContentType = ContentType;
            context.Response.StatusCode = (int)getResponseCode(exception);

            string errorResponseJsoned = getUserFriendlyErrorResponse(exception, context.TraceIdentifier, idpTraceId);
            await context.Response.WriteAsync(errorResponseJsoned, Encoding.UTF8);
        }

        private static HttpStatusCode getResponseCode(Exception error)
        {
            if (error is BaseBusinessException)
                return HttpStatusCode.BadRequest;
            else
            {       // FinLib.Common.Exceptions.Base.InfraException baashe ya System.Exception
                return HttpStatusCode.InternalServerError;
                // dar har soorat bayad hame error haa ro handle konim o ta jayi k mishe InternalServerErrors nadim b client, vali dg gaahi karsh nmishe kard :(
            }
        }

        private static string getUserFriendlyErrorResponse(Exception error, string contextTraceId, string idpTraceId)
        {
            string errorMessage;

            if (error is BaseBusinessException)
            {
                // is UserFriendly Exception
                errorMessage = error.Message;
            }
            else
            {
                errorMessage = $"An error occurred when processing your request. Please contact the support team" +
                    $"{Environment.NewLine}{Environment.NewLine}" +
                    $"Error Id:" +
                    $"{Environment.NewLine}" +
                    $"{idpTraceId}";
            }

            var errorResponse = new JsonResult()
            {
                ContextTraceId = contextTraceId,
                IdpTraceId = idpTraceId,
#if DEBUG
                Error = error,
#endif
                Success = false,
                Message = errorMessage
            };

            return errorResponse.ToJson();
        }
    }
}
