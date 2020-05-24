using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTrackerWeb.Models;

namespace TimeTrackerWeb.Controllers
{
    public class AuthenticationController : BaseDataAccessController<UserModel>
    {
        private const string TimeTrackerApiSubPath = "api/Authentication/Login";

        // GET: Authentication/Index
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await PostWithResultAsync(TimeTrackerApiSubPath, model);

            HttpContext.Session.SetString("authenticationToken", result);

            return RedirectToAction("Index", "Home");
        }
    }
}