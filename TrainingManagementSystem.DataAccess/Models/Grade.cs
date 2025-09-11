using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManagementSystem.DataAccess.Models
{
    public class Grade
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Session is required.")]
        public int SessionId { get; set; }

        [Required(ErrorMessage = "Trainee is required.")]
        public int TraineeId { get; set; }
        [Required(ErrorMessage = "Instructor is required.")]
        public int GradeById { get; set; }

        [Required(ErrorMessage = "Grade is required")]
        [Range(0, 100, ErrorMessage = "Grade must be between 0 and 100")]
        public decimal Value { get; set; }
        public string? Comments { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Session Session { get; set; }
        public User Trainee { get; set; }
        public User GradeBy{ get; set; }  // Instructor who assigned the grade

        // Soft Delete
        public bool IsDeleted { get; set; } = false;
    }
}
