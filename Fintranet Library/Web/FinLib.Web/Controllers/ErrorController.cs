using FinLib.Common.Exceptions.Base;
using FinLib.Common.Extensions;
using FinLib.Providers.Logging;
using FinLib.Web.Shared.Models.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinLib.Web.Controllers
{
    public class ErrorController : Base.BaseAnonymousController
    {
        /// <summary>
        /// Global Handling for the unhandled Exceptions in our internal business
        /// خطاهایی که از سمت کلاینت درخواست ممکنه نشده باشن
        /// و استاتوس کد خطا مثل 500، ندارن
        /// ولی عملا یجور 
        /// InternalServerError
        /// هستن
        /// </summary>
        /// <returns></returns>
        public IActionResult HandleUnhandledException([FromServices] IAppLogger logger)
        {
            var errorMessage = "با پوزش بابت اشکال بوجود آمده، خواهشمند است جهت  رفع آن، شناسه رهگیری درج شده در پایین متن را به پشتیبانی سامانه اطلاع دهید";

            var theError = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var idpTraceId = logger.Error(FinLib.Models.Enums.EventCategory.Application
                , FinLib.Models.Enums.EventId.UnhandledException,
                  FinLib.Models.Enums.EventType.Error
                  , errorMessage, theError.Error, customData: theError.Path);

            var theModel = generateModelForResponseCode500(errorMessage, idpTraceId);

            return View("Error", model: theModel);
        }

        //[Route("Error/{statusCode}")]
        public IActionResult HandleStatusCodedError([FromServices] IAppLogger logger, int? statusCode = null)
        {
            return statusCode switch
            {
                StatusCodes.Status400BadRequest => handleStatusCode400(logger),
                StatusCodes.Status404NotFound => handleStatusCode404(logger),
                StatusCodes.Status405MethodNotAllowed => handleStatusCode405(logger),
                StatusCodes.Status500InternalServerError => handleStatusCode500(logger),

                _ => hanldeUnknownError(logger, statusCode)
            };
        }

        private IActionResult hanldeUnknownError(IAppLogger logger, int? statusCode)
        {
            var theErrorFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            var traceId = logger.Fatal( FinLib.Models.Enums.EventCategory.Application
                , FinLib.Models.Enums.EventId.UnhandledException, FinLib.Models.Enums.EventType.Error
                , "خطایی در اجرا عملیات درخواستی رخ داده است", customData: theErrorFeature.ToJson());

            var theModel = new GeneralErrorViewModel
            {
                Title = "خطای کد " + statusCode + " در اجرا عملیات",
                ContextTraceId = HttpContext.TraceIdentifier,
                TraceId = traceId,
                Message = "لطفا با پشتیبانی سامانه تماس حاصل فرمایید"
            };

            return View("Error", model: theModel);
        }

        private IActionResult handleStatusCode400( IAppLogger logger)
        {
            var theErrorFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            var traceId = logger.Error
                ( FinLib.Models.Enums.EventCategory.Application
, FinLib.Models.Enums.EventId.BadRequest,  FinLib.Models.Enums.EventType.Error
, "فرمت درخواست/پارامترهای ارسالی  نامعتبر می باشد", customData: theErrorFeature.ToJson());

            var theModel = new GeneralErrorViewModel
            {
                Title = "اشکال در درخواست ارسالی",
                ContextTraceId = HttpContext.TraceIdentifier,
                TraceId = traceId,
                Message = "اشکالی در درخواست ارسالی از سمت شما وجود دارد؛ لطفا با پشتیبانی سامانه تماس حاصل فرمایید"
            };

            return View("Error", model: theModel);
        }

        /// <summary>
        /// Not Found
        /// صفحه یا آدرس اشتباه درخواست داده شده باشه از سمت کاربر
        /// </summary>
        /// <param name="loggingService"></param>
        /// <returns></returns>
        private IActionResult handleStatusCode404(IAppLogger logger)
        {
            var theErrorFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            var traceId = logger.Error
                (FinLib.Models.Enums.EventCategory.Application
, FinLib.Models.Enums.EventId.ResourceNotFound, FinLib.Models.Enums.EventType.Error
, "Resource not found", customData: theErrorFeature.ToJson());

            var theModel = new GeneralErrorViewModel
            {
                Title = "چنین صفحه ای در این سامانه وجود ندارد",
                Message = "لطفا آدرس وارد شده را مجددا بررسی بفرمایید، یا در صورتی که احتمال می دهید مشکل بوجود آمده از سمت سامانه می باشد، شناسه ی رهگیری درج شده در پایین متن را به پشتیبانی سامانه اطلاع دهید",
                ContextTraceId = HttpContext.TraceIdentifier,
                TraceId = traceId,
            };

            return View("Error", model: theModel);
        }

        /// <summary>
        /// Method not allowed
        /// </summary>
        /// <param name="loggingService"></param>
        /// <returns></returns>
        private IActionResult handleStatusCode405(IAppLogger logger)
        {
            var theErrorFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            var traceId = logger.Error
                (FinLib.Models.Enums.EventCategory.Application
, FinLib.Models.Enums.EventId.MethodNotAllowed, FinLib.Models.Enums.EventType.Error
, "Method not allowed", customData: theErrorFeature.ToJson());

            var theModel = new GeneralErrorViewModel
            {
                Title = "خطا در دسترسی اجرا عملیات",
                ContextTraceId = HttpContext.TraceIdentifier,
                TraceId = traceId,
                Message = "امکان اجرا عملیات درخواستی وجود ندارد"
            };

            return View("Error", model: theModel);
        }

        /// <summary>
        /// Internal Server Error
        /// اکسپشن هایی که از داخل بیزینس برنامه هندل نشده و به بیرون میخاد درز پیدا کنه
        /// اینا باید به مرور هی هندل بشه و خطای مناسب تر (مثلا 400) صادر بشه یا چیزی غیر از 500
        /// </summary>
        /// <param name="loggingService"></param>
        /// <returns></returns>
        private IActionResult handleStatusCode500(IAppLogger logger)
        {
            var errorMessage = "خطایی در اجرا عملیات درخواستی شما رخ داده است، لطفا جهت بررسی و رفع آن، شناسه رهگیری درج شده در پایین متن را به پشتیبانی سامانه اطلاع دهید";

            var theErrorFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var traceId = logger.Error
                (FinLib.Models.Enums.EventCategory.Application
, FinLib.Models.Enums.EventId.UnhandledException
, FinLib.Models.Enums.EventType.Error
, errorMessage, customData: theErrorFeature.ToJson());


            var theModel = generateModelForResponseCode500(errorMessage, traceId);

            return View("Error", model: theModel);
        }

        private GeneralErrorViewModel generateModelForResponseCode500(string errorMessage, string idpTraceId)
        {
            var theModel = new GeneralErrorViewModel();

            theModel.Title = "اشکالی در پردازش درخواست شما رخ داده است!";
            theModel.Message = errorMessage;
            theModel.ContextTraceId = HttpContext.TraceIdentifier;
            theModel.TraceId = idpTraceId;

            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionFeature != null && exceptionFeature.Error is BaseBusinessException)
            {
                theModel.Title = "اشکالی در پردازش درخواست شما رخ داده است";
                theModel.Message = exceptionFeature.Error.Message;
            }

            return theModel;
        }
    }
}
