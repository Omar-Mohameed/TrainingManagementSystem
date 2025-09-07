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
    public class GradeRepository : GenericRepository<Grade>, IGradeRepository
    {
        private readonly AppDbContext _context;
        public GradeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Grade> GetGradesByTrainee(int traineeId, int page, int pageSize, out int totalCount)
        {
            var query = _context.Grades
                .Include(g => g.Session)
                .Where(g => g.TraineeId == traineeId);

            totalCount = query.Count();

            return query
                .OrderByDescending(g => g.Session.StartDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public bool GradeExists(int traineeId, int sessionId)
        {
            return _context.Grades.Any(g => g.TraineeId == traineeId && g.SessionId == sessionId);
        }
    }
}
