using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManagementSystem.DataAccess.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Code is required")]
        [StringLength(20)]
        public string Code { get; set; }

        [Required(ErrorMessage = "Course name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Course name must be between 3 and 50 characters.")]
        [Remote(action: "IsCourseNameUnique", controller: "Course", AdditionalFields = "Id", ErrorMessage = "Course name already exists.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Course Category is required.")]
        public string Category { get; set; }

        [Range(1, 10, ErrorMessage = "Credits must be between 1 and 10")]
        public int Credits { get; set; }
        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "Instructor is required.")]
        public int InstructorId { get; set; }  // FK
        public User Instructor { get; set; }
        public ICollection<Session> Sessions { get; set; }

    }
}
