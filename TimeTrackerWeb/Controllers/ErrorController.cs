using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Sartain_Studios_Common.Logging;
using TimeTrackerWeb.Models;

namespace TimeTrackerWeb.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILoggerWrapper _loggerWrapper;

        public ErrorController(ILoggerWrapper loggerWrapper)
        {
            _loggerWrapper = loggerWrapper;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorReason = DetermineErrorReason();

            if (errorReason.IsAuthorizationIssue)
                return RedirectToAction("Login", "Authentication");

            return View(errorReason);
        }

        private ErrorViewModel DetermineErrorReason()
        {
            var error = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var httpRequestFeature = HttpContext.Features.Get<IHttpRequestFeature>();

            var exception = error.Error;

            _loggerWrapper.LogError(exception.Message, GetType().Name, nameof(Error) + "()", null);
            _loggerWrapper.LogError(exception.InnerException.Message, GetType().Name, nameof(Error) + "()", null);

            var path = httpRequestFeature.RawTarget;
            var httpMethod = Request.Method;
            var userFriendlyMessage = exception.Message;
            var stackTrace = exception.StackTrace;
            var innerException = exception.InnerException?.ToString() != null
                ? exception.InnerException.ToString()
                : "None";

            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            var isAuthorizationIssue = false;

            if (innerException.Contains("Unauthorized") || userFriendlyMessage.Contains("Unauthorized"))
                isAuthorizationIssue = true;

            return new ErrorViewModel
            {
                RequestId = requestId,
                Path = path,
                HttpMethod = httpMethod,
                UserFriendlyMessage = userFriendlyMessage,
                StackTrace = stackTrace,
                InnerException = innerException,
                IsAuthorizationIssue = isAuthorizationIssue
            };
        }
    }
}