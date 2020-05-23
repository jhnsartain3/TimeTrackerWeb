using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeTrackerWeb.Models;

namespace TimeTrackerWeb.Controllers
{
    public class ProjectController : BaseDataAccessController<ProjectModel>
    {
        private const string TimeTrackerApiSubPath = "api/Project";

        // GET: Project
        public async Task<IActionResult> Index()
        {
            return View(await GetAll(TimeTrackerApiSubPath));
        }

        // GET: Project/Details/5
        public async Task<IActionResult> Details(string id)
        {
            return View(await GetById(TimeTrackerApiSubPath, id));
        }

        // GET: Project/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await Post(TimeTrackerApiSubPath, model);

            return RedirectToAction(nameof(Index));
        }

        // GET: Project/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            return View(await GetById(TimeTrackerApiSubPath, id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ProjectModel model)
        {
            if (!id.Equals(model.Id)) return NotFound();

            if (!ModelState.IsValid) return View(model);

            await Update(TimeTrackerApiSubPath, id, model);

            return RedirectToAction(nameof(Index));
        }

        // GET: Project/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            return View(await GetById(TimeTrackerApiSubPath, id));
        }

        // POST: Project/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await DeleteById(TimeTrackerApiSubPath, id);

            return RedirectToAction(nameof(Index));
        }
    }
}
