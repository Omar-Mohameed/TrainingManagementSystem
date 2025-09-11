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
        public SessionSearchViewModel GetSessions(string searchTerm, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.Sessions.GetAll(includeProperties: "Course,Instructor");
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var lowerSearchTerm = searchTerm.ToLower();
                query = query.Where(s => s.Course.Name.ToLower().Contains(searchTerm)
                                    || s.Title.ToLower().Contains(lowerSearchTerm));
            }
            var totalRecords = query.Count();

            var sessions = query
                .OrderBy(s => s.StartDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new SessionVM
                {
                    Id = s.Id,
                    Title = s.Title,
                    CourseId = s.CourseId,
                    CourseName = s.Course.Name,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    Mode = s.Mode,
                    Location = s.Location,
                    InstructorId = s.InstructorId,
                    InstructorName = s.Instructor != null ? s.Instructor.Name : "N/A"
                })
                .ToList();
            return new SessionSearchViewModel
            {
                Sessions = sessions,
                SearchCourseName = searchTerm,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
            };
        }
        public SessionVM GetSessionById(int id)
        {
            var session = _unitOfWork.Sessions.GetFirstOrDefault(s => s.Id == id, includeProperties: "Course,Instructor");
            if (session == null) return null;
            return new SessionVM
            {
                Id = session.Id,
                Title = session.Title,
                CourseId = session.CourseId,
                CourseName = session.Course.Name,
                StartDate = session.StartDate,
                EndDate = session.EndDate,
                Mode = session.Mode,
                Location = session.Location,
                InstructorId = session.InstructorId,
                InstructorName = session.Instructor != null ? session.Instructor.Name : "N/A"
            };
        }
        public void CreateSession(SessionVM model)
        {
            var session = new Session
            {
                Title = model.Title,
                CourseId = model.CourseId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Mode = model.Mode,
                Location = model.Location,
                InstructorId = model.InstructorId,
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
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
                .ToList();
        }
        public IEnumerable<SelectListItem> GetInstructorsDropdown()
        {
            return _unitOfWork.Users.GetAll()
                .Where(u => u.Role == "Instructor")
                .OrderBy(u => u.Name)
                .Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
                .ToList();
        }
        public SessionVM GetCreateVM()
        {
            return new SessionVM
            {
                Courses = GetCoursesDropdown(),
                Instructors = GetInstructorsDropdown()
            };
        }

        public bool UpdateSession(SessionVM model)
        {
            var session = _unitOfWork.Sessions.GetFirstOrDefault(s => s.Id == model.Id, includeProperties: "Course,Instructor");
            if (session == null) return false;

            session.Title = model.Title;
            session.CourseId = model.CourseId;
            session.InstructorId = model.InstructorId;
            session.StartDate = model.StartDate;
            session.EndDate = model.EndDate;
            session.Mode = model.Mode;
            session.Location = model.Location;


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

        public bool IsTitleUnique(string title, int id)
        {
            var existingSession = _unitOfWork.Sessions.GetFirstOrDefault(s => s.Title == title && s.Id != id);
            return existingSession == null;
        }

        public void DeleteSessionSoft(int id)
        {
            var sesstion = _unitOfWork.Sessions.GetFirstOrDefault(s => s.Id == id);
            if (sesstion != null)
            {
                _unitOfWork.Sessions.SoftDelete(sesstion);
                _unitOfWork.Save();
            }
        }
    }
}
