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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public ICourseRepository Courses { get; private set; }
        public IUserRepository Users { get; private set; }
        public ISessionRepository Sessions { get; private set; }
        public IGradeRepository Grades { get; private set; }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Courses = new CourseRepository(_context);
            Users = new UserRepository(_context);
            Sessions = new SessionRepository(_context);
            Grades = new GradeRepository(_context);
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
