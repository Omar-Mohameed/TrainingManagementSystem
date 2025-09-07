using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManagementSystem.Business.CustomValidators;

namespace TrainingManagementSystem.Business.ViewModels
{
    public class CourseVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Course name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Course name must be between 3 and 50 characters.")]
        [Remote(action: "IsCourseNameUnique", controller: "Course", AdditionalFields = "Id", ErrorMessage = "Course name already exists.")]
        [NoNumber(ErrorMessage = "Course name cannot contain numbers.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Course description is required.")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Instructor is required.")]
        [Display(Name = "Instructor")]
        public int InstructorId { get; set; }
        public string? InstructorName { get; set; }

        // For Dropdown
        public IEnumerable<SelectListItem>? Instructors { get; set; }
    }
}
