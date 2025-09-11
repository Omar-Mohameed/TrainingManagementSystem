using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManagementSystem.Business.ViewModels;

namespace TrainingManagementSystem.Business.Services.Interfaces
{
    public interface ICourseService
    {
        //IEnumerable<CourseVM> GetAll(string searchWord = null);
        CourseIndexViewModel GetCourses(string searchTerm, int pageNumber = 1, int pageSize = 5);
        CourseVM GetById(int id);
        void Create(CourseVM model);
        void Update(CourseVM model);
        void Delete(int id);
        void DeleteCourseSoft(int id); // Soft Delete

        bool IsCourseNameUnique(string name, int id);
        bool IsCourseCodeUnique(string code, int id);
    }
}
