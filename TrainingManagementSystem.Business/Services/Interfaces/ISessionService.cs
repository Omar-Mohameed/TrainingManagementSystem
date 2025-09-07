using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManagementSystem.Business.ViewModels;

namespace TrainingManagementSystem.Business.Services.Interfaces
{
    public interface ISessionService
    {
        // List + Search + Pagination
        SessionSearchViewModel GetSessions(string searchCourseName, int pageNumber, int pageSize);
        SessionVM GetSessionById(int id);
        void CreateSession(SessionVM model);
        bool UpdateSession(SessionVM model);
        bool DeleteSession(int id);
        // Helpers
        IEnumerable<SelectListItem> GetCoursesDropdown();
        SessionVM GetCreateVM();
    }
}
