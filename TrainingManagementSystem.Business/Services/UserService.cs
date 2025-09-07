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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _unitOfWork.Users.GetAll();
        }

        public IEnumerable<User> GetAllInstructors()
        {
            return _unitOfWork.Users.GetAllInstructors();
        }

        public IEnumerable<User> GetAllTrainees()
        {
            return _unitOfWork.Users.GetAllTrainees();
        }
        public User GetUserById(int id)
        {
            return _unitOfWork.Users.GetFirstOrDefault(u => u.Id == id);
        }
        public void CreateUser(UserVM userVM)
        {
            var user = new User
            {
                Name = userVM.Name,
                Email = userVM.Email,
                Role = userVM.Role
            };
            _unitOfWork.Users.Add(user);
            _unitOfWork.Save();
        }

        public void UpdateUser(UserVM userVM)
        {
            var user = _unitOfWork.Users.GetFirstOrDefault(u => u.Id == userVM.Id);
            user.Name = userVM.Name;
            user.Email = userVM.Email;
            user.Role = userVM.Role;
            _unitOfWork.Users.Update(user);
            _unitOfWork.Save();
        }
        public void DeleteUser(int id)
        {
            var user = _unitOfWork.Users.GetFirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _unitOfWork.Users.Delete(id);
                _unitOfWork.Save();
            }
        }

        public UserSearchViewModel GetUsers(string searchName, string searchRole, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.Users.GetAll();

            // بحث بالاسم
            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(u => u.Name.Contains(searchName));
            }

            // بحث بالدور
            if (!string.IsNullOrEmpty(searchRole))
            {
                query = query.Where(u => u.Role == searchRole);
            }

            var totalRecords = query.Count();

            var users = query
                .OrderBy(u => u.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new UserVM
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role
                })
                .ToList();

            return new UserSearchViewModel
            {
                Users = users,
                SearchName = searchName,
                SearchRole = searchRole,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
            };
        }

        public bool IsEmailUnique(string email, int id)
        {
            var existingUser = _unitOfWork.Users.GetFirstOrDefault(u => u.Email == email && u.Id != id);
            return existingUser == null;
        }
    }
}
