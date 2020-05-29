using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTrackerWeb.Models;

namespace TimeTrackerWeb.Controllers
{
    public class AuthenticationController : BaseDataAccessController<UserModel>
    {
        private const string TimeTrackerLoginApiSubPath = "api/Authentication/Login";
        private const string TimeTrackerSignUpApiSubPath = "api/Authentication/SignUp";
        private const string TimeTrackerUserInformationApiSubPath = "api/UserInformation";

        // GET: Authentication/Login
        public IActionResult Login()
        {
            var userName = HttpContext.Session.GetString("username");
            ViewBag.username = userName;

            return View();
        }

        // GET: Authentication/Login
        public IActionResult SignUp()
        {
            return View();
        }

        // GET: Authentication/Logout
        public IActionResult Logout()
        {
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

                HttpContext.Session.SetString("authenticationToken", token);
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to login", exception);
            }

            try
            {
                var userInformation = await GetById(TimeTrackerUserInformationApiSubPath, model.Username,
                    GetAuthenticationTokenFromSession());

                HttpContext.Session.SetString("username", userInformation.Username);
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to retrieve use details", exception);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(UserModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var wasSuccessfullyCreated = await PostWithResultAsync(TimeTrackerSignUpApiSubPath, model);

                HttpContext.Session.SetString("username", model.Username);
            }
            catch (Exception exception)
            {
                throw new Exception("Something went wrong during registration", exception);
            }

            return RedirectToAction("Login", "Authentication");
        }

        private string GetAuthenticationTokenFromSession()
        {
            return HttpContext.Session.GetString("authenticationToken");
        }
    }
}