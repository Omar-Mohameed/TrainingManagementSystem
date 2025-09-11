using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManagementSystem.DataAccess.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [Remote(action: "IsEmailUnique", controller: "User", AdditionalFields = "Id", ErrorMessage = "Email already exists.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Role is required.")]
        [Display(Name = "User Role")]
        public string Role { get; set; } // Admin, Instructor, Trainee
        public bool IsActive { get; set; } = true;
        public ICollection<Grade> Grades { get; set; }
        public ICollection<Course> Courses { get; set; }

        // Soft Delete
        public bool IsDeleted { get; set; } = false;

    }
}
