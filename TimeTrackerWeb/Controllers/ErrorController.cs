using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Sartain_Studios_Common.Logging;
using System.Diagnostics;
using TimeTrackerWeb.Models;

namespace TimeTrackerWeb.Controllers
{
    public class ErrorController : Controller
    {
        private ILoggerWrapper _loggerWrapper;

        public ErrorController(ILoggerWrapper loggerWrapper)
        {
            _loggerWrapper = loggerWrapper;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var error = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var httpRequestFeature = HttpContext.Features.Get<IHttpRequestFeature>();

            var exception = error.Error;

            _loggerWrapper.LogError(exception.Message, this.GetType().Name, nameof(Error) + "()", null);
            _loggerWrapper.LogError(exception.InnerException.Message, this.GetType().Name, nameof(Error) + "()", null);

            var path = httpRequestFeature.RawTarget;
            var httpMethod = Request.Method;
            var userFriendlyMessage = exception.Message;
            var stackTrace = exception.StackTrace;
            var innerException = exception.InnerException?.ToString() != null
                ? exception.InnerException.ToString()
                : "None";

            if (innerException.Contains("Unauthorized") || userFriendlyMessage.Contains("Unauthorized"))
                return RedirectToAction("Login", "Authentication");

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