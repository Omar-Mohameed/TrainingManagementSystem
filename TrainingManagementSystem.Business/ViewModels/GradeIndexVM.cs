using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManagementSystem.Business.ViewModels
{
    public class GradeIndexVM
    {
        public IEnumerable<GradeVM> Grades { get; set; } = new List<GradeVM>();
        public string SearchTerm { get; set; } = string.Empty;
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
