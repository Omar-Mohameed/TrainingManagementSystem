using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using TrainingManagementSystem.Business.Services;
using TrainingManagementSystem.Business.Services.Interfaces;
using TrainingManagementSystem.Business.ViewModels;

namespace TrainingManagementSystem.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        public IActionResult Index(string searchName, string searchRole, int pageNumber = 1)
        {
            var model = userService.GetUsers(searchName, searchRole, pageNumber);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new UserVM());
        }
        [HttpPost]
        public IActionResult Create(UserVM model)
        {
            if (ModelState.IsValid)
            {
                userService.CreateUser(model);
                TempData["SuccessMessage"] = "User created successfully!";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = userService.GetUserById(id);
            var userVM = new UserVM
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive
            };

            return View(userVM);
        }
        [HttpPost]
        public IActionResult Edit(UserVM model)
        {
            if (ModelState.IsValid)
            {
                userService.UpdateUser(model);
                TempData["SuccessMessage"] = "User updated successfully!";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var user = userService.GetAllUsers().FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return Json(new { success = false, message = "Course not found!" });
            }
            userService.DeleteUser(id);
            return Json(new { success = true, message = "Course deleted successfully!" });
        }
        // Remote Validation for Unique Email
        public IActionResult IsEmailUnique(string email, int id)
        {
            bool isUnique = userService.IsEmailUnique(email, id);
            if (!isUnique)
            {
                return Json($"Email '{email}' is already taken.");
            }

            return Json(true);
        }
    }
}
