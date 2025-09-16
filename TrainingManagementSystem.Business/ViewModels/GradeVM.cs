using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManagementSystem.Business.ViewModels
{
    public class GradeVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Session is required.")]
        public int SessionId { get; set; }
        public string? SessionName { get; set; }

        [Required(ErrorMessage = "Trainee is required.")]
        public int TraineeId { get; set; }
        public string? TraineeName { get; set; }
        [Required(ErrorMessage = "Instructor is required.")]
        public int GradeById { get; set; }
        public string? InstructorName { get; set; }

        [Required(ErrorMessage = "Grade is required")]
        [Range(1, 100, ErrorMessage = "Grade must be between 1 and 100")]
        public decimal Value { get; set; }
        public string? Comments { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;

        // Dropdown lists
        public IEnumerable<SelectListItem> Sessions { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Trainees { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Instructors { get; set; } = new List<SelectListItem>();
    }

    /*
     id   |   Trainee Name     |    Session   | Grade
      1   |        Omar        |      C#          50
      2   |     ahmed          |     c           60
     */
}
