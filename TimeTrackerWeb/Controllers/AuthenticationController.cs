using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTrackerWeb.Models;

namespace TimeTrackerWeb.Controllers
{
    public class AuthenticationController : BaseDataAccessController<UserModel>
    {
        private const string TimeTrackerLoginApiSubPath = "api/Authentication/Login";
        private const string TimeTrackerUserInformationApiSubPath = "api/UserInformation";

        // GET: Authentication/Login
        public IActionResult Login()
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

            var token = await PostWithResultAsync(TimeTrackerLoginApiSubPath, model);

            HttpContext.Session.SetString("authenticationToken", token);

            var userInformation = await GetById(TimeTrackerUserInformationApiSubPath, model.Username,
                GetAuthenticationTokenFromSession());

            HttpContext.Session.SetString("username", userInformation.Username);

            return RedirectToAction("Index", "Home");
        }

        private string GetAuthenticationTokenFromSession()
        {
            return HttpContext.Session.GetString("authenticationToken");
        }
    }
}