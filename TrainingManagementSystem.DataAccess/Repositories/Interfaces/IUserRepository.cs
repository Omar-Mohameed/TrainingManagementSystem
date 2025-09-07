using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManagementSystem.DataAccess.Models;

namespace TrainingManagementSystem.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        void Update(User user);
        IEnumerable<User> GetAllInstructors();
        IEnumerable<User> GetAllTrainees();
    }
}
