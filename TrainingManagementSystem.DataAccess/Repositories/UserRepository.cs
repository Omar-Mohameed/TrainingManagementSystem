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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public void Update(User user)
        {
            var UserInDb = context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (UserInDb != null)
            {
                UserInDb.Name = user.Name;
                UserInDb.Email = user.Email;
                UserInDb.Role = user.Role;
            }
        }
        public IEnumerable<User> GetAllInstructors()
        {
            return context.Users.Where(u => u.Role == "Instructor").ToList();
        }

        public IEnumerable<User> GetAllTrainees()
        {
            return context.Users.Where(u => u.Role == "Trainee").ToList();
        }
    }
}
