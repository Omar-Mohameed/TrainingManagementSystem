using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManagementSystem.DataAccess.Models;

namespace TrainingManagementSystem.Business.ViewModels
{
    public class UserSearchViewModel
    {
        public IEnumerable<UserVM> Users { get; set; }

        // For search
        public string SearchName { get; set; }
        public string SearchRole { get; set; }

        // For pagination
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
