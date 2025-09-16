using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
    public class GradeService : IGradeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GradeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public GradeIndexVM GetAllGrades(string searchTerm , int pageNumber, int pageSize=5)
        {
            var query = _unitOfWork.Grades.GetAll(
                        filter: g => !g.IsDeleted,
                        includeProperties: "Session,Trainee,GradeBy");

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();

                query = query.Where(g =>
                    g.Session.Title.ToLower().Contains(searchTerm) ||
                    g.Trainee.Name.ToLower().Contains(searchTerm) 
                );
            }
            int totalCount = query.Count();

            var grades = query
            .OrderByDescending(g => g.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(g => new GradeVM
            {
                Id = g.Id,
                SessionName = g.Session.Title,
                TraineeName = g.Trainee.Name,
                GradeById = g.GradeBy.Id,
                InstructorName = g.GradeBy.Name,
                Value = g.Value,
                Comments = g.Comments,
                CreateAt = g.CreatedAt
            })
            .ToList();
            return new GradeIndexVM
            {
                Grades = grades,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                SearchTerm = searchTerm
            };
        }
        public IEnumerable<GradeVM> GetGradesForTrainee(int traineeId, int pageNumber, int pageSize=5)
        {
            var totalRecords = _unitOfWork.Grades.GetGradesCountByTrainee(traineeId);

            var grades = _unitOfWork.Grades.GetGradesByTrainee(traineeId, pageNumber, pageSize);

            return grades.Select(g => new GradeVM
            {
                Id = g.Id,
                SessionName = g.Session.Title,
                TraineeName = g.Trainee.Name,
                InstructorName = g.GradeBy.Name,
                Value = g.Value,
                Comments = g.Comments,
                CreateAt = g.CreatedAt
            });
        }
        public GradeVM GetGradeById(int id)
        {
            var grade = _unitOfWork.Grades.GetFirstOrDefault(
            filter: g => g.Id == id && !g.IsDeleted,
            includeProperties: "Session,Trainee,GradeBy");
            if (grade == null) return null;
            return new GradeVM
            {
                Id = grade.Id,
                SessionId = grade.SessionId,
                SessionName = grade.Session.Title,
                TraineeId = grade.TraineeId,
                TraineeName = grade.Trainee.Name,
                GradeById = grade.GradeById,
                InstructorName = grade.GradeBy.Name,
                Value = grade.Value,
                Comments = grade.Comments,
                CreateAt = grade.CreatedAt,
                Sessions = GetSessionsDropdown(),
                Trainees = GetTraineesDropdown(),
                Instructors = GetInstructorsDropdown()
            };
        }
        public void CreateGrade(GradeVM model)
        {
            var grade = new Grade
            {
                SessionId = model.SessionId,
                TraineeId = model.TraineeId,
                GradeById = model.GradeById,
                Value = model.Value,
                Comments = model.Comments,
                CreatedAt = DateTime.Now,
            };

            _unitOfWork.Grades.Add(grade);
            _unitOfWork.Save();
        }
        public void UpdateGrade(GradeVM gradeVM)
        {
            var grade = _unitOfWork.Grades.GetFirstOrDefault(g => g.Id == gradeVM.Id);

            grade.Value = gradeVM.Value;
            grade.Comments = gradeVM.Comments;
            grade.TraineeId = gradeVM.TraineeId;
            grade.SessionId = gradeVM.SessionId;
            grade.GradeById = gradeVM.GradeById;


            _unitOfWork.Grades.Update(grade);
            _unitOfWork.Save();
        }

        public void DeleteGradeSoft(int id)
        {
            var grade = _unitOfWork.Grades.GetFirstOrDefault(g => g.Id == id);
            if (grade != null)
            {
                _unitOfWork.Grades.SoftDelete(grade);
                _unitOfWork.Save();
            }
        }

        public GradeVM GetCreateVM()  // return view model with dropdown Lists for sessions, trainees, instructors
        {
            return new GradeVM
            {
                Sessions = GetSessionsDropdown(),

                Trainees = GetTraineesDropdown(),

                Instructors = GetInstructorsDropdown()
            };
        }
        public IEnumerable<SelectListItem> GetSessionsDropdown()
        {
            return _unitOfWork.Sessions.GetAll(s => !s.IsDeleted).OrderBy(s=>s.Title)
                    .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Title }).ToList();
        }
        public IEnumerable<SelectListItem> GetTraineesDropdown()
        {
            return _unitOfWork.Users.GetAll(u => u.Role == "Trainee" && !u.IsDeleted).OrderBy(u=>u.Name)
                    .Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Name }).ToList();
        }
        public IEnumerable<SelectListItem> GetInstructorsDropdown()
        {
            return _unitOfWork.Users.GetAll(u => u.Role == "Instructor" && !u.IsDeleted).OrderBy(u=>u.Name)
                    .Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Name }).ToList();
        }
    }
}
