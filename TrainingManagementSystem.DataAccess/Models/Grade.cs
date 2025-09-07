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
        [Required(ErrorMessage = "Grade is required")]
        [Range(0, 100, ErrorMessage = "Grade must be between 0 and 100")]
        public int Value { get; set; }
        [Required(ErrorMessage = "Session is required.")]
        public int SessionId { get; set; }
        public Session Session { get; set; }
        [Required(ErrorMessage = "Trainee is required.")]
        public int TraineeId { get; set; }
        public User Trainee { get; set; }
    }
}
