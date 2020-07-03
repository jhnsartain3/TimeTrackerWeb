using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sartain_Studios_Common.Logging;
using System;
using System.Threading.Tasks;
using TimeTrackerWeb.Models;

namespace TimeTrackerWeb.Controllers
{
    public class EventController : BaseDataAccessController<EventModel>
    {
        private const string TimeTrackerApiSubPath = "api/Event";

        public EventController(ILoggerWrapper loggerWrapper) : base(loggerWrapper) { }

        // GET: Event/5
        public async Task<IActionResult> Index(string id)
        {
            ViewBag.itemId = id;

            _loggerWrapper.LogInformation("id: " + id, this.GetType().Name, nameof(Index) + "()", null);

            return View(await GetAllById<EventModelWithQuantitiesOfTimeAndDateTimeInfoHeaders>(TimeTrackerApiSubPath + "/GetAllByIdWithQuantitiesOfTimeAndDateTimeInfoHeaders", id,
                GetAuthenticationTokenFromSession()));
        }

        // GET: Event/Details/5
        public async Task<IActionResult> Details(string id)
        {
            _loggerWrapper.LogInformation("id: " + id, this.GetType().Name, nameof(Details) + "()", null);

            return View(await GetById(TimeTrackerApiSubPath, id, GetAuthenticationTokenFromSession()));
        }

        // GET: Event/Create/5
        public IActionResult Create(string id)
        {
            _loggerWrapper.LogInformation("id: " + id, this.GetType().Name, nameof(Create) + "()", null);

            var dateTime = DateTime.Now;

            return View(new EventModel
            {
                ProjectId = id,
                StartDateTime = dateTime.Date.AddHours(dateTime.Hour).AddMinutes(dateTime.Minute)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventModel model)
        {
            if (!ModelState.IsValid) return View(model);

            _loggerWrapper.LogInformation("model.UserId: " + model.UserId, this.GetType().Name, nameof(Create) + "()", null);

            // Model's Id value seems to be magically set to the Model's Projectid
            model.Id = null;

            await Post(TimeTrackerApiSubPath, model, GetAuthenticationTokenFromSession());

            return RedirectToAction(nameof(Index), new { id = model.ProjectId });
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            _loggerWrapper.LogInformation("id: " + id, this.GetType().Name, nameof(Edit) + "()", null);

            return View(await GetById(TimeTrackerApiSubPath, id, GetAuthenticationTokenFromSession()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EventModel model)
        {
            if (!id.Equals(model.Id)) return NotFound();

            if (!ModelState.IsValid) return View(model);

            _loggerWrapper.LogInformation("id: " + id, this.GetType().Name, nameof(Edit) + "()", null);

            await Update(TimeTrackerApiSubPath, id, model, GetAuthenticationTokenFromSession());

            return RedirectToAction(nameof(Index), new { id = model.ProjectId });
        }

        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            _loggerWrapper.LogInformation("id: " + id, this.GetType().Name, nameof(Delete) + "()", null);

            return View(await GetById(TimeTrackerApiSubPath, id, GetAuthenticationTokenFromSession()));
        }

        // POST: Event/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id, string projectId)
        {
            _loggerWrapper.LogInformation("id: " + id + " " + "projectId: " + projectId, this.GetType().Name, nameof(DeleteConfirmed) + "()", null);

            await DeleteById(TimeTrackerApiSubPath, id, GetAuthenticationTokenFromSession());

            return RedirectToAction(nameof(Index), new { id = projectId });
        }

        // GET: Event/Stop/5
        public async Task<IActionResult> Stop(string id)
        {
            _loggerWrapper.LogInformation("id: " + id, this.GetType().Name, nameof(Stop) + "()", null);

            var itemModel = await GetById(TimeTrackerApiSubPath, id, GetAuthenticationTokenFromSession());

            if (!id.Equals(itemModel.Id)) return NotFound();

            var dateTime = DateTime.Now;
            itemModel.EndDateTime = dateTime.Date.AddHours(dateTime.Hour).AddMinutes(dateTime.Minute);

            await Update(TimeTrackerApiSubPath, id, itemModel, GetAuthenticationTokenFromSession());

            return RedirectToAction(nameof(Index), new { id = itemModel.ProjectId });
        }

        // GET: Event/Start/5
        public async Task<IActionResult> Start(string id)
        {
            _loggerWrapper.LogInformation("id: " + id, this.GetType().Name, nameof(Start) + "()", null);

            var dateTime = DateTime.Now;

            var itemModel = new EventModel
            {
                StartDateTime = dateTime.Date.AddHours(dateTime.Hour).AddMinutes(dateTime.Minute),
                ProjectId = id
            };

            await Post(TimeTrackerApiSubPath, itemModel, GetAuthenticationTokenFromSession());

            return RedirectToAction(nameof(Index), new { id = itemModel.ProjectId });
        }

        private string GetAuthenticationTokenFromSession()
        {
            var authenticationToken = HttpContext.Session.GetString("authenticationToken");

            _loggerWrapper.LogInformation(authenticationToken, this.GetType().Name, nameof(GetAuthenticationTokenFromSession) + "()", null);

            return authenticationToken;
        }
    }
}