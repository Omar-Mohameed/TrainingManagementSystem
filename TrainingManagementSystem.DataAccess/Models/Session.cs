using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManagementSystem.DataAccess.Models
{
    public class Session
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters.")]
        [Remote(action: "IsTitleUnique", controller: "Session", AdditionalFields = nameof(Id), ErrorMessage = "This session title already exists.")]
        [Display(Name = "Session Title")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.Now;
        [Required]
        [StringLength(20)]
        public string Mode { get; set; } // Online / Offline / Hybrid
        [StringLength(200)]
        public string? Location { get; set; }
        [Required(ErrorMessage = "Course is required.")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        [Required(ErrorMessage = "Instructor is required.")]
        public int InstructorId { get; set; }
        public User Instructor { get; set; }

        public ICollection<Grade> Grades { get; set; }

        // Soft Delete
        public bool IsDeleted { get; set; } = false;
    }
}
