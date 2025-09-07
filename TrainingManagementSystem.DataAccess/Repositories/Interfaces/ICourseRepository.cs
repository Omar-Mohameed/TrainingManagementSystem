using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManagementSystem.DataAccess.Models;

namespace TrainingManagementSystem.DataAccess.Repositories.Interfaces
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        void Update(Course course);
        bool IsCourseNameUnique(string name, int id);
    }
}
