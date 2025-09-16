using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainingManagementSystem.Business.Services;
using TrainingManagementSystem.Business.Services.Interfaces;
using TrainingManagementSystem.Business.ViewModels;

namespace TrainingManagementSystem.UI.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService courseService;
        private readonly IUserService userService;

        public CourseController(ICourseService courseService, IUserService userService)
        {
            this.courseService = courseService;
            this.userService = userService;
        }
        // Get All
        public IActionResult Index(string searchTerm, int pageNumber = 1)
        {
            var coursesIndexVM = courseService.GetCourses(searchTerm, pageNumber);
            return View(coursesIndexVM);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var course = courseService.GetById(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var courseVM = new CourseVM
            {
                Instructors = userService.GetAllInstructors()
                                        .Select(i => new SelectListItem
                                        {
                                            Value = i.Id.ToString(),
                                            Text = i.Name
                                        })
            };
            return View(courseVM);
        }
        [HttpPost]
        public IActionResult Create(CourseVM courseVM)
        {
            if(ModelState.IsValid)
            {
                courseService.Create(courseVM);
                TempData["SuccessMessage"] = "Course created successfully.";
                return RedirectToAction("Index");
            }
            return View(courseVM);
        }

        #region Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var courseVM = courseService.GetById(id);
            courseVM.Instructors = userService.GetAllInstructors()
                                        .Select(i => new SelectListItem
                                        {
                                            Value = i.Id.ToString(),
                                            Text = i.Name
                                        });
            if (courseVM == null) return NotFound();
            return View(courseVM);
        }
        [HttpPost]
        public IActionResult Edit(CourseVM courseVM)
        {
            if(ModelState.IsValid)
            {
                courseService.Update(courseVM);
                TempData["SuccessMessage"] = "Course updated successfully.";
                return RedirectToAction("Index");
            }
            return View(courseVM);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int id)
        {
            var course = courseService.GetById(id);
            if (course == null)
            {
                return Json(new { success = false, message = "Course not found!" });
            }
            courseService.DeleteCourseSoft(id);
            return Json(new { success = true, message = "Course deleted successfully!" });
        }
        #endregion

        // Check Unique Name
        public IActionResult IsCourseNameUnique(string name, int id)
        {
            var isUnique = courseService.IsCourseNameUnique(name, id);
            return Json(isUnique);
        }
        // Check Unique Code
        public IActionResult IsCourseCodeUnique(string code, int id)
        {
            var isUnique = courseService.IsCourseCodeUnique(code, id);
            if(!isUnique)
            {
                return Json($"The course code '{code}' is already taken.");
            }
            return Json(true);
        }
    }
}
