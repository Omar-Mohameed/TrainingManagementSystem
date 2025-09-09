using Microsoft.AspNetCore.Mvc;
using TrainingManagementSystem.Business.Services.Interfaces;
using TrainingManagementSystem.Business.ViewModels;

namespace TrainingManagementSystem.UI.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;
        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        public IActionResult Index(string searchCourseName, int pageNumber = 1, int pageSize = 5)
        {
            var model = _sessionService.GetSessions(searchCourseName, pageNumber, pageSize);
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var sessionVM = _sessionService.GetCreateVM();
            return View(sessionVM);
        }
        [HttpPost]
        public IActionResult Create(SessionVM model)
        {
            if (ModelState.IsValid)
            {
                _sessionService.CreateSession(model);
                TempData["SuccessMessage"] = "Session Created successfully!";
                return RedirectToAction("Index");
            }
            model.Courses = _sessionService.GetCoursesDropdown();
            model.Instructors = _sessionService.GetInstructorsDropdown();
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var sessionVM = _sessionService.GetSessionById(id);
            if (sessionVM == null)
            {
                return NotFound();
            }
            sessionVM.Courses = _sessionService.GetCoursesDropdown();
            sessionVM.Instructors = _sessionService.GetInstructorsDropdown();
            return View(sessionVM);
        }
        [HttpPost]
        public IActionResult Edit(SessionVM model)
        {
            if (ModelState.IsValid)
            {
                var updated = _sessionService.UpdateSession(model);
                if (updated)
                {
                    TempData["SuccessMessage"] = "Session updated successfully!";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Error while updating session");
            }
            model.Courses = _sessionService.GetCoursesDropdown();
            model.Instructors = _sessionService.GetInstructorsDropdown();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleted = _sessionService.DeleteSession(id);
            if (!deleted)
            {
                return Json(new { success = false, message = "Error while deleting session" });
            }

            return Json(new { success = true, message = "Session deleted successfully" });
        }

        // Remote Validation for StartDate
        public IActionResult ValidateStartDate(DateTime startDate)
        {
            if (_sessionService.ValidateStartDate(startDate))
            {
                return Json(true);
            }
            return Json("Start date cannot be in the past.");
        }
        // Remote Validation for EndDate
        public IActionResult ValidateEndDate(DateTime startDate, DateTime endDate)
        {
            if (_sessionService.ValidateEndDate(startDate, endDate))
            {
                return Json(true);
            }
            return Json("End date must be after Start Date.");
        }
    }
}
