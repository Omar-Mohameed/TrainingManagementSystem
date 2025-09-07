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
        [Required]
        [Range(0, 100)]
        public int Value { get; set; }

        public int SessionId { get; set; }
        public Session Session { get; set; }

        public int TraineeId { get; set; }
        public User Trainee { get; set; }
    }
}
