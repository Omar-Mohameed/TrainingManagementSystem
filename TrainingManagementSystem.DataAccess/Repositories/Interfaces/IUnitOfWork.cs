using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManagementSystem.DataAccess.Models;

namespace TrainingManagementSystem.DataAccess.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        ICourseRepository Courses { get; }
        IUserRepository Users { get; }
        ISessionRepository Sessions { get; }
        IGenericRepository<Grade> Grades { get; }
        int Save();
    }
}
