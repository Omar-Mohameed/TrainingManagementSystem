using Microsoft.AspNetCore.Mvc;

namespace TrainingManagementSystem.UI.Controllers
{
    public class GradeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

/*
 Trainee Name     |    Session   | Grade
    Omar                C#          50
    ahmed               c           60
 */
