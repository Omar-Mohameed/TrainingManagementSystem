using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Printing;
using TrainingManagementSystem.Business.Services.Interfaces;
using TrainingManagementSystem.Business.ViewModels;
using TrainingManagementSystem.DataAccess.Repositories.Interfaces;

namespace TrainingManagementSystem.UI.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeService _gradeService;


        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }
        public IActionResult Index(string searchTerm, int pageNumber = 1)
        {
            var grades = _gradeService.GetAllGrades(searchTerm,pageNumber);
            return View(grades);
        }

        // Create Grade
        [HttpGet]
        public IActionResult Create()
        {
            var model = _gradeService.GetCreateVM();  // return view model with dropdown Lists for sessions, trainees, instructors
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(GradeVM model)
        {
            if (ModelState.IsValid)
            {
                _gradeService.CreateGrade(model);

                TempData["Success"] = "Grade recorded successfully!";
                return RedirectToAction("Index", new { traineeId = model.TraineeId });
            }

            model.Sessions = _gradeService.GetSessionsDropdown();
            model.Trainees = _gradeService.GetTraineesDropdown();
            model.Instructors = _gradeService.GetInstructorsDropdown();
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var gradevm = _gradeService.GetGradeById(id);
            if (gradevm == null)
            {
                return NotFound();
            }
            return View(gradevm);
        }
        [HttpPost]
        public IActionResult Edit(GradeVM model)
        {
            if (ModelState.IsValid)
            {
                _gradeService.UpdateGrade(model);
                TempData["SuccessMessage"] = "Grade updated successfully!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var grade = _gradeService.GetGradeById(id);
            if (grade == null)
            {
                return Json(new { success = false, message = "Grade not found!" });
            }
            _gradeService.DeleteGradeSoft(id);
            return Json(new { success = true, message = "Grade deleted successfully!" });
        }
    }
}

/*
 Trainee Name     |    Session   | Grade
    Omar                C#          50
    ahmed               c           60
 */
