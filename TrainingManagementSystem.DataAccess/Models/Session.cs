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
        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Course is required.")]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public ICollection<Grade> Grades { get; set; }
    }
}
