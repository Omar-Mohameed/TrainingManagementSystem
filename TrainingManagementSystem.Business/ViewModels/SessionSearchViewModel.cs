using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManagementSystem.Business.ViewModels
{
    public class SessionSearchViewModel
    {
        public IEnumerable<SessionVM> Sessions { get; set; }
        public string SearchCourseName { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
