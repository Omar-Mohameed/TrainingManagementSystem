using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManagementSystem.Business.ViewModels;
using TrainingManagementSystem.DataAccess.Models;

namespace TrainingManagementSystem.Business.Services.Interfaces
{
    public interface IUserService
    {
        UserSearchViewModel GetUsers(string searchName, string searchRole, int pageNumber, int pageSize=5);
        IEnumerable<User> GetAllUsers();
        IEnumerable<User> GetAllInstructors();
        IEnumerable<User> GetAllTrainees();
        User GetUserById(int id);
        void CreateUser(UserVM userVM);
        void UpdateUser(UserVM userVM);
        void DeleteUser(int id);
        bool IsEmailUnique(string email, int id);

    }
}
