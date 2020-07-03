using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sartain_Studios_Common.Logging;
using System;
using System.Threading.Tasks;
using TimeTrackerWeb.Models;

namespace TimeTrackerWeb.Controllers
{
    public class AuthenticationController : BaseDataAccessController<UserModel>
    {
        private const string TimeTrackerLoginApiSubPath = "api/Authentication/Login";
        private const string TimeTrackerSignUpApiSubPath = "api/Authentication/SignUp";
        private const string TimeTrackerUserInformationApiSubPath = "api/UserInformation";

        public AuthenticationController(ILoggerWrapper loggerWrapper) : base(loggerWrapper) { }

        // GET: Authentication/Login
        public IActionResult Login()
        {
            var userName = HttpContext.Session.GetString("username");

            _loggerWrapper.LogInformation("HttpContext saved username: " + userName, this.GetType().Name, nameof(Login) + "()", null);

            ViewBag.username = userName;

            return View();
        }

        // GET: Authentication/Login
        public IActionResult SignUp()
        {
            _loggerWrapper.LogInformation("Sign Up", this.GetType().Name, nameof(SignUp) + "()", null);

            return View();
        }

        // GET: Authentication/Logout
        public IActionResult Logout()
        {
            _loggerWrapper.LogInformation("Logging out", this.GetType().Name, nameof(Logout) + "()", null);

            _loggerWrapper.LogInformation("Logging out username: " + HttpContext.Session.GetString("username"), this.GetType().Name, nameof(Logout) + "()", null);

            HttpContext.Session.Remove("authenticationToken");
            HttpContext.Session.Remove("username");

            return RedirectToAction("Login", "Authentication");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var token = await PostWithResultAsync(TimeTrackerLoginApiSubPath, model);

                _loggerWrapper.LogInformation("authenticationToken: " + token, this.GetType().Name, nameof(Login) + "()", null);

                HttpContext.Session.SetString("authenticationToken", token);
            }
            catch (Exception exception)
            {
                _loggerWrapper.LogError("Unable to login: " + model.Username + " " + model.UserId, this.GetType().Name, nameof(Login) + "()", null);
                _loggerWrapper.LogError(exception.Message, this.GetType().Name, nameof(Login) + "()", null);
                _loggerWrapper.LogError(exception.InnerException.Message, this.GetType().Name, nameof(Login) + "()", null);

                throw new Exception("Unable to login", exception);
            }

            try
            {
                var userInformation = await GetById(TimeTrackerUserInformationApiSubPath, model.Username,
                    GetAuthenticationTokenFromSession());

                _loggerWrapper.LogInformation("username: " + userInformation, this.GetType().Name, nameof(Login) + "()", null);

                HttpContext.Session.SetString("username", userInformation.Username);
            }
            catch (Exception exception)
            {
                _loggerWrapper.LogError("Unable to retrieve use details: " + model.Username + " " + model.UserId, this.GetType().Name, nameof(Login) + "()", null);
                _loggerWrapper.LogError(exception.Message, this.GetType().Name, nameof(Login) + "()", null);
                _loggerWrapper.LogError(exception.InnerException.Message, this.GetType().Name, nameof(Login) + "()", null);

                throw new Exception("Unable to retrieve use details", exception);
            }

            _loggerWrapper.LogInformation("Login Completed", this.GetType().Name, nameof(Login) + "()", null);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(UserModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var resultMessage = await PostWithResultAsync(TimeTrackerSignUpApiSubPath, model);

                _loggerWrapper.LogInformation(resultMessage, this.GetType().Name, nameof(SignUp) + "()", null);

                if (resultMessage.Equals("User " + model.Username + " created successfully"))
                {
                    HttpContext.Session.SetString("username", model.Username);

                    ViewBag.accountCreatedSuccessfully = true;

                    return View();
                }
                {
                    ModelState.AddModelError(string.Empty, "Could not create user. Try again later");

                    _loggerWrapper.LogError("Could not create user. Try again later", this.GetType().Name, nameof(SignUp) + "()", null);

                    return View();
                }

            }
            catch (Exception exception)
            {
                if (exception.InnerException != null && exception.InnerException.Message.Contains("The value is already in use: " + model.Username))
                {
                    _loggerWrapper.LogInformation("Username already taken: " + nameof(UserModel.Username), this.GetType().Name, nameof(SignUp) + "()", null);

                    ModelState.AddModelError(nameof(UserModel.Username), "Username already taken");

                    return View();
                }

                _loggerWrapper.LogError("Something went wrong during registration: " + nameof(UserModel.Username), this.GetType().Name, nameof(SignUp) + "()", null);
                _loggerWrapper.LogError(exception.Message, this.GetType().Name, nameof(SignUp) + "()", null);

                throw new Exception("Something went wrong during registration", exception);
            }
        }

        private string GetAuthenticationTokenFromSession()
        {
            var authenticationToken = HttpContext.Session.GetString("authenticationToken");

            _loggerWrapper.LogInformation(authenticationToken, this.GetType().Name, nameof(GetAuthenticationTokenFromSession) + "()", null);

            return authenticationToken;
        }
    }
}