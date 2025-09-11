using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManagementSystem.Business.ViewModels
{
    public class SessionVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters.")]
        [Remote(action: "IsTitleUnique",controller: "Session",AdditionalFields = nameof(Id),ErrorMessage = "This session title already exists.")]
        [Display(Name = "Session Title")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Course is required.")]
        [Display(Name = "Course")]
        public int CourseId { get; set; }

        public string? CourseName { get; set; } // للعرض فقط

        [Required(ErrorMessage = "Instructor is required.")]
        [Display(Name = "Instructor")]
        public int InstructorId { get; set; }
        public string? InstructorName { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date)]
        [Remote(action: "ValidateStartDate", controller: "Session", AdditionalFields = "EndDate", ErrorMessage = "Start date cannot be in the past.")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date)]
        [Remote(action: "ValidateEndDate", controller: "Session", AdditionalFields = "StartDate", ErrorMessage = "End date must be after Start Date.")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; } = DateTime.Now;
        public string Mode { get; set; } // Online / Offline / Hybrid
        public string? Location { get; set; }

        // Dropdown for Courses
        public IEnumerable<SelectListItem>? Courses { get; set; }
        public IEnumerable<SelectListItem>? Instructors { get; set; }
    }
}
