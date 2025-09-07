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

        [Required(ErrorMessage = "Course is required.")]
        [Display(Name = "Course")]
        public int CourseId { get; set; }

        public string? CourseName { get; set; } // للعرض فقط

        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date)]
        [Remote(action: "ValidateStartDate", controller: "Session", AdditionalFields = "EndDate", ErrorMessage = "Start date cannot be in the past.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date)]
        [Remote(action: "ValidateEndDate", controller: "Session", AdditionalFields = "StartDate", ErrorMessage = "End date must be after Start Date.")]
        public DateTime EndDate { get; set; }

        // Dropdown for Courses
        public IEnumerable<SelectListItem>? Courses { get; set; }
    }
}
