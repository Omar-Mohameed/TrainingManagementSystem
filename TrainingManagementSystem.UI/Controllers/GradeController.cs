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
        private readonly IUnitOfWork _unitOfWork;


        public GradeController(IGradeService gradeService, IUnitOfWork unitOfWork)
        {
            _gradeService = gradeService;
            _unitOfWork = unitOfWork;
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
            var model = new GradeVM
            {
                Sessions = _unitOfWork.Sessions.GetAll(s => !s.IsDeleted)
                    .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Title }),

                Trainees = _unitOfWork.Users.GetAll(u => u.Role == "Trainee" && !u.IsDeleted)
                    .Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Name }),

                Instructors = _unitOfWork.Users.GetAll(u => u.Role == "Instructor" && !u.IsDeleted)
                    .Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Name })
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Create(GradeVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Sessions = _unitOfWork.Sessions.GetAll(s => !s.IsDeleted)
                    .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Title });

                model.Trainees = _unitOfWork.Users.GetAll(u => u.Role == "Trainee" && !u.IsDeleted)
                    .Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Name });

                model.Instructors = _unitOfWork.Users.GetAll(u => u.Role == "Instructor" && !u.IsDeleted)
                    .Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Name });

                return View(model);
            }
            _gradeService.CreateGrade(model);

            TempData["Success"] = "Grade recorded successfully!";
            return RedirectToAction("Index", new { traineeId = model.TraineeId });
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
