using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NLog;
using Pigeon.Framework.Exceptions;
using Pigeon.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Pigeon.WebApi.WebApi
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        protected static Logger Logger = LogManager.GetCurrentClassLogger();
        public override void OnException(ExceptionContext context)
        {
            Logger.Error(context.Exception);

            ApiError apiError = null;
            if (context.Exception is ApiException)
            {
                // handle explicit 'known' API errors
                var ex = context.Exception as ApiException;
                context.Exception = null;
                apiError = new ApiError(ex.HttpStatusCode, ex.Title, ex.Detail);                

                context.HttpContext.Response.StatusCode = (int)ex.HttpStatusCode;
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                apiError = new ApiError(HttpStatusCode.Unauthorized, "Unauthorized Access");
                context.HttpContext.Response.StatusCode = 401;
            }
            else
            {
                // Unhandled errors
#if !DEBUG
                var msg = "An unhandled error occurred.";                
                string stack = null;
#else
                var msg = context.Exception.GetBaseException().Message;
                string stack = context.Exception.StackTrace;
#endif

                apiError = new ApiError(HttpStatusCode.InternalServerError, "The request failed due to an internal error.");                
                context.HttpContext.Response.StatusCode = 500;
                // handle logging here
            }
                       
            // always return a JSON result
            context.Result = new JsonResult(apiError);

            base.OnException(context);
        }
    }
}
