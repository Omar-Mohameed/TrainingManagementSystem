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
        IEnumerable<CourseVM> GetAll(string searchWord = null);
        CourseVM GetById(int id);
        void Create(CourseVM model);
        void Update(CourseVM model);
        void Delete(int id);
        bool IsCourseNameUnique(string name, int id);

    }
}
