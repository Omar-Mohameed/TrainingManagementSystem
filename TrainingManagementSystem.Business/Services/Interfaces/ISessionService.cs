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
        SessionSearchViewModel GetSessions(string searchCourseName, int pageNumber, int pageSize=5);
        SessionVM GetSessionById(int id);
        void CreateSession(SessionVM model);
        bool UpdateSession(SessionVM model);
        bool DeleteSession(int id); // Hard Delete
        void DeleteSessionSoft(int id); // Soft Delete
        // Helpers
        IEnumerable<SelectListItem> GetCoursesDropdown();
        IEnumerable<SelectListItem> GetInstructorsDropdown();
        SessionVM GetCreateVM();

        // For Remote Validation
        bool ValidateStartDate(DateTime startDate);
        bool ValidateEndDate(DateTime startDate, DateTime endDate);
        bool IsTitleUnique(string title, int id);
    }
}
