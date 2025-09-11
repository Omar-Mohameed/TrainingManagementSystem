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
        public IEnumerable<Grade> GetGradesByTrainee(int traineeId, int pageNumber, int pageSize)
        {
            return _context.Grades
                .Include(g => g.Session)
                .Include(g => g.Trainee)
                .Include(g => g.GradeBy)
                .Where(g => g.TraineeId == traineeId && !g.IsDeleted)
                .OrderByDescending(g => g.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
        public int GetGradesCountByTrainee(int traineeId)
        {
            return _context.Grades.Count(g => g.TraineeId == traineeId && !g.IsDeleted);
        }

        public bool GradeExists(int traineeId, int sessionId)
        {
            return _context.Grades.Any(g => g.TraineeId == traineeId && g.SessionId == sessionId);
        }

        public void Update(Grade grade)
        {
            var GradeInDb = _context.Grades.FirstOrDefault(g => g.Id == grade.Id);
            if (GradeInDb != null)
            {
                GradeInDb.SessionId = grade.SessionId;
                GradeInDb.TraineeId = grade.TraineeId;
                GradeInDb.GradeById = grade.GradeById;
                GradeInDb.Value = grade.Value;
                GradeInDb.Comments = grade.Comments;
                GradeInDb.CreatedAt = DateTime.Now;
            }
        }
    }
}
