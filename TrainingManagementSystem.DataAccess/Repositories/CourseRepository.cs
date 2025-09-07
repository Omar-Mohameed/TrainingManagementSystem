using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManagementSystem.DataAccess.Data;
using TrainingManagementSystem.DataAccess.Models;
using TrainingManagementSystem.DataAccess.Repositories.Interfaces;

namespace TrainingManagementSystem.DataAccess.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly AppDbContext context;

        public CourseRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        
        public void Update(Course course)
        {
            var CourseInDb = context.Courses.FirstOrDefault(c => c.Id == course.Id);
            if (CourseInDb != null)
            {
                CourseInDb.Name = course.Name;
                CourseInDb.Category = course.Category;
                CourseInDb.InstructorId = course.InstructorId;
            }
        }
        public bool IsCourseNameUnique(string name, int id)
        {
            return !context.Courses.Any(c => c.Name.ToLower() == name.ToLower() && c.Id != id);
        }
    }
}
