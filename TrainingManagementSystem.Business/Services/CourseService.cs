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
        public IEnumerable<CourseVM> GetAll(string searchWord = null)
        {
            var courses = unitOfWork.Courses.GetAll(includeProperties: "Instructor");

            if (!string.IsNullOrEmpty(searchWord))
            {
                courses = courses.Where(c => c.Name.Contains(searchWord, StringComparison.OrdinalIgnoreCase)
                                          || c.Category.Contains(searchWord, StringComparison.OrdinalIgnoreCase));
            }

            return courses.Select(c => new CourseVM
            {
                Id = c.Id,
                Name = c.Name,
                Category = c.Category,
                InstructorId = c.InstructorId,
                InstructorName = c.Instructor != null ? c.Instructor.Name : ""
            }).ToList();
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
                InstructorName = course.Instructor?.Name
            };
        }
        public void Create(CourseVM courseVM)
        {
            var course = new Course
            {
                Name = courseVM.Name,
                Category = courseVM.Category ,
                InstructorId = courseVM.InstructorId
            };
            unitOfWork.Courses.Add(course);
            unitOfWork.Save();
        }
        public void Update(CourseVM courseVM)
        {
            var course = new Course
            {
                Id = courseVM.Id,
                Name = courseVM.Name,
                Category = courseVM.Category,
                InstructorId = courseVM.InstructorId
            };
            unitOfWork.Courses.Update(course);
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
