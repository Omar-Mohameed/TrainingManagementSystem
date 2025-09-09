using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManagementSystem.Business.ViewModels
{
    public class CourseIndexViewModel
    {
        public IEnumerable<CourseVM> Courses { get; set; } = new List<CourseVM>();
        public string SearchTerm { get; set; } = string.Empty;
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
