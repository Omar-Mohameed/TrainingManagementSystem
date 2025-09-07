using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManagementSystem.Business.Services.Interfaces;
using TrainingManagementSystem.Business.ViewModels;
using TrainingManagementSystem.DataAccess.Models;
using TrainingManagementSystem.DataAccess.Repositories.Interfaces;

namespace TrainingManagementSystem.Business.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public SessionSearchViewModel GetSessions(string searchCourseName, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.Sessions.GetAll(includeProperties: "Course");
            if (!string.IsNullOrEmpty(searchCourseName))
            {
                query = query.Where(s => s.Course.Name.Contains(searchCourseName));
            }
            var totalRecords = query.Count();

            var sessions = query
                .OrderBy(s => s.StartDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new SessionVM
                {
                    Id = s.Id,
                    CourseId = s.CourseId,
                    CourseName = s.Course.Name,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate
                })
                .ToList();
            return new SessionSearchViewModel
            {
                Sessions = sessions,
                SearchCourseName = searchCourseName,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
            };
        }
        public SessionVM GetSessionById(int id)
        {
            var session = _unitOfWork.Sessions.GetFirstOrDefault(s => s.Id == id, includeProperties: "Course");
            if (session == null) return null;
            return new SessionVM
            {
                Id = session.Id,
                CourseId = session.CourseId,
                CourseName = session.Course.Name,
                StartDate = session.StartDate,
                EndDate = session.EndDate
            };
        }
        public void CreateSession(SessionVM model)
        {
            var session = new Session
            {
                CourseId = model.CourseId,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };

            _unitOfWork.Sessions.Add(session);
            _unitOfWork.Save();
        }

        public bool DeleteSession(int id)
        {
            var session = _unitOfWork.Sessions.GetFirstOrDefault(s => s.Id == id);
            if (session == null) return false;

            _unitOfWork.Sessions.Delete(id);
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<SelectListItem> GetCoursesDropdown()
        {
            return _unitOfWork.Courses.GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
                .ToList();
        }

        public SessionVM GetCreateVM()
        {
            return new SessionVM
            {
                Courses = GetCoursesDropdown()
            };
        }

        public bool UpdateSession(SessionVM model)
        {
            var session = _unitOfWork.Sessions.GetFirstOrDefault(s => s.Id == model.Id);
            if (session == null) return false;

            session.CourseId = model.CourseId;
            session.StartDate = model.StartDate;
            session.EndDate = model.EndDate;

            _unitOfWork.Sessions.Update(session);
            _unitOfWork.Save();
            return true;
        }

        // Remote Validation
        public bool ValidateStartDate(DateTime startDate)
        {
            // Start date must not be in the past
            return startDate.Date >= DateTime.Today;
        }
        public bool ValidateEndDate(DateTime startDate, DateTime endDate)
        {
            // End date must be after start date
            return endDate.Date > startDate.Date;
        }
    }
}
