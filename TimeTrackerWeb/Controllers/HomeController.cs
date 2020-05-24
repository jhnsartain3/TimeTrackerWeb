using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TimeTrackerWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Project");
        }

        public IActionResult About()
        {
            return View();
        }
    }
}