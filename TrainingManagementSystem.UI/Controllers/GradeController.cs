using Microsoft.AspNetCore.Mvc;
using TrainingManagementSystem.Business.Services.Interfaces;

namespace TrainingManagementSystem.UI.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeService gradeService;

        public GradeController(IGradeService gradeService)
        {
            this.gradeService = gradeService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            var grade = gradeService.GetAllGrades().FirstOrDefault(g => g.Id == id);
            if (grade == null)
            {
                return Json(new { success = false, message = "Grade not found!" });
            }
            gradeService.DeleteGradeSoft(id);
            return Json(new { success = true, message = "Grade deleted successfully!" });
        }
    }
}

/*
 Trainee Name     |    Session   | Grade
    Omar                C#          50
    ahmed               c           60
 */
