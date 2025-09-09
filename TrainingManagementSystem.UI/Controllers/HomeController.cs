using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrainingManagementSystem.DataAccess.Data;
using TrainingManagementSystem.DataAccess.Models;

namespace TrainingManagementSystem.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext context;
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Courses = context.Courses.Count();
            ViewBag.Users = context.Users.Count();
            ViewBag.Sessions = context.Sessions.Count();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
