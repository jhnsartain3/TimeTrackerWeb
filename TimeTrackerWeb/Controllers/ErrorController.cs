using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using TimeTrackerWeb.Models;

namespace TimeTrackerWeb.Controllers
{
    public class ErrorController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var error = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var httpRequestFeature = HttpContext.Features.Get<IHttpRequestFeature>();

            var exception = error.Error;

            var path = httpRequestFeature.RawTarget;
            var httpMethod = Request.Method;
            var userFriendlyMessage = exception.Message;
            var stackTrace = exception.StackTrace;
            var innerException = exception.InnerException?.ToString() != null
                ? exception.InnerException.ToString()
                : "None";

            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            return View(new ErrorViewModel
            {
                RequestId = requestId,
                Path = path,
                HttpMethod = httpMethod,
                UserFriendlyMessage = userFriendlyMessage,
                StackTrace = stackTrace,
                InnerException = innerException
            });
        }
    }
}