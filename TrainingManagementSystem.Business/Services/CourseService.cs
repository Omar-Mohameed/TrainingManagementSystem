using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManagementSystem.Business.Services.Interfaces;
using TrainingManagementSystem.Business.ViewModels;
using TrainingManagementSystem.DataAccess.Models;
using TrainingManagementSystem.DataAccess.Repositories;
using TrainingManagementSystem.DataAccess.Repositories.Interfaces;

namespace TrainingManagementSystem.Business.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        //public IEnumerable<CourseVM> GetAll(string searchWord = null)
        //{
        //    var courses = unitOfWork.Courses.GetAll(includeProperties: "Instructor");

        //    if (!string.IsNullOrEmpty(searchWord))
        //    {
        //        courses = courses.Where(c => c.Name.Contains(searchWord, StringComparison.OrdinalIgnoreCase)
        //                                  || c.Category.Contains(searchWord, StringComparison.OrdinalIgnoreCase));
        //    }

        //    return courses.Select(c => new CourseVM
        //    {
        //        Id = c.Id,
        //        Name = c.Name,
        //        Category = c.Category,
        //        InstructorId = c.InstructorId,
        //        InstructorName = c.Instructor != null ? c.Instructor.Name : ""
        //    }).ToList();
        //}


        public CourseIndexViewModel GetCourses(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            var query = unitOfWork.Courses.GetAll(includeProperties: "Instructor");

            // Search functionality (Ignor Case)
            if (!string.IsNullOrEmpty(searchTerm))
            {
                string lowerSearchTerm = searchTerm.ToLower();

                query = query.Where(c => c.Name.ToLower().Contains(lowerSearchTerm)
                                      || c.Category.ToLower().Contains(lowerSearchTerm)
                                      || c.Code.ToLower().Contains(lowerSearchTerm));
            }

            var totalCourses = query.Count();
            var totalPages = (int)Math.Ceiling(totalCourses / (double)pageSize);

            var courses = query
                .OrderBy(c => c.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CourseVM
                {
                    Id = c.Id,
                    Code = c.Code,
                    Name = c.Name,
                    Category = c.Category,
                    InstructorName = c.Instructor.Name,
                    Credits = c.Credits,
                    IsActive = c.IsActive
                })
                .ToList();

            return new CourseIndexViewModel
            {
                Courses = courses,
                SearchTerm = searchTerm,
                CurrentPage = pageNumber,
                TotalPages = totalPages
            };
        }
        public CourseVM GetById(int id)
        {
            var course = unitOfWork.Courses.GetFirstOrDefault(c => c.Id == id, includeProperties: "Instructor");
            if (course == null) return null;

            return new CourseVM
            {
                Id = course.Id,
                Name = course.Name,
                Category = course.Category,
                InstructorId = course.InstructorId,
                InstructorName = course.Instructor?.Name,
                Code = course.Code,
                Credits = course.Credits,
                IsActive = course.IsActive
            };
        }
        public void Create(CourseVM courseVM)
        {
            var course = new Course
            {
                Name = courseVM.Name,
                Code = courseVM.Code,
                Credits = courseVM.Credits,
                Category = courseVM.Category ,
                InstructorId = courseVM.InstructorId,
                IsActive = courseVM.IsActive
            };
            unitOfWork.Courses.Add(course);
            unitOfWork.Save();
        }
        public void Update(CourseVM courseVM)
        {
            var course = unitOfWork.Courses.GetFirstOrDefault(c => c.Id == courseVM.Id);
            if (course == null)
            {
                throw new Exception("Course not found");
            }

            course.Name = courseVM.Name;
            course.Code = courseVM.Code;
            course.Credits = courseVM.Credits;
            course.Category = courseVM.Category;
            course.InstructorId = courseVM.InstructorId;
            course.IsActive = courseVM.IsActive;

            unitOfWork.Save();
        }
        public void Delete(int id)
        {
            unitOfWork.Courses.Delete(id);
            unitOfWork.Save();
        }
        public bool IsCourseNameUnique(string name, int id)
        {
            return unitOfWork.Courses.IsCourseNameUnique(name, id);
        }

    }
}
