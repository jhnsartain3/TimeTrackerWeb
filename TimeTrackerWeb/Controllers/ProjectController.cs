using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sartain_Studios_Common.Logging;
using System.Threading.Tasks;
using TimeTrackerWeb.Models;

namespace TimeTrackerWeb.Controllers
{
    public class ProjectController : BaseDataAccessController<ProjectModel>
    {
        private const string TimeTrackerApiSubPath = "api/Project";

        public ProjectController(ILoggerWrapper loggerWrapper) : base(loggerWrapper) { }

        // GET: Project
        public async Task<IActionResult> Index()
        {
            _loggerWrapper.LogInformation("index", this.GetType().Name, nameof(Index) + "()", null);

            return View(await GetAll(TimeTrackerApiSubPath + "/GetAllWithQuantitiesOfTime", GetAuthenticationTokenFromSession()));
        }

        // GET: Project/Details/5
        public async Task<IActionResult> Details(string id)
        {
            _loggerWrapper.LogInformation("id: " + id, this.GetType().Name, nameof(Details) + "()", null);

            return View(await GetById<ProjectModelWithQuantitiesOfTime>(TimeTrackerApiSubPath + "/GetByIdWithQuantitiesOfTimeAsync", id, GetAuthenticationTokenFromSession()));
        }

        // GET: Project/Details/5
        public async Task<double> GetTotalProjectHoursSinceLastSunday(string id)
        {
            _loggerWrapper.LogInformation("id: " + id, this.GetType().Name, nameof(GetTotalProjectHoursSinceLastSunday) + "()", null);

            return await GetById<double>(TimeTrackerApiSubPath + "/GetTotalProjectHoursSinceLastSunday", id, GetAuthenticationTokenFromSession());
        }

        // GET: Project/Create
        public IActionResult Create()
        {
            _loggerWrapper.LogInformation("Create", this.GetType().Name, nameof(Create) + "()", null);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectModel model)
        {
            if (!ModelState.IsValid) return View(model);

            _loggerWrapper.LogInformation("model.UserId: " + model.UserId, this.GetType().Name, nameof(Create) + "()", null);

            await Post(TimeTrackerApiSubPath, model, GetAuthenticationTokenFromSession());

            return RedirectToAction(nameof(Index));
        }

        // GET: Project/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            _loggerWrapper.LogInformation("id: " + id, this.GetType().Name, nameof(Edit) + "()", null);

            return View(await GetById(TimeTrackerApiSubPath, id, GetAuthenticationTokenFromSession()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ProjectModel model)
        {
            if (!id.Equals(model.Id)) return NotFound();

            if (!ModelState.IsValid) return View(model);

            _loggerWrapper.LogInformation("id: " + id, this.GetType().Name, nameof(Edit) + "()", null);

            await Update(TimeTrackerApiSubPath, id, model, GetAuthenticationTokenFromSession());

            return RedirectToAction(nameof(Index));
        }

        // GET: Project/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            _loggerWrapper.LogInformation("id: " + id, this.GetType().Name, nameof(Delete) + "()", null);

            return View(await GetById(TimeTrackerApiSubPath, id, GetAuthenticationTokenFromSession()));
        }

        // POST: Project/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            _loggerWrapper.LogInformation("id: " + id, this.GetType().Name, nameof(DeleteConfirmed) + "()", null);

            await DeleteById(TimeTrackerApiSubPath, id, GetAuthenticationTokenFromSession());

            return RedirectToAction(nameof(Index));
        }

        private string GetAuthenticationTokenFromSession()
        {
            var authenticationToken = HttpContext.Session.GetString("authenticationToken");

            _loggerWrapper.LogInformation(authenticationToken, this.GetType().Name, nameof(GetAuthenticationTokenFromSession) + "()", null);

            return authenticationToken;
        }
    }
}