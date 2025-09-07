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
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly AppDbContext context;
        public SessionRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(Session session)
        {
            var SessionInDb = context.Sessions.FirstOrDefault(s => s.Id == session.Id);
            if (SessionInDb != null)
            {
                SessionInDb.StartDate = session.StartDate;
                SessionInDb.EndDate = session.EndDate;
                SessionInDb.CourseId = session.CourseId;
            }
        }
        public IEnumerable<Session> GetUpcomingSessions()
        {
            return context.Sessions
                .Include(s => s.Course)
                .Where(s => s.StartDate >= System.DateTime.Today)
                .OrderBy(s => s.StartDate)
                .ToList();
        }
        public IEnumerable<Session> GetSessionsByCourseId(int courseId)
        {
            return context.Sessions
                            .Include(s => s.Course)
                            .Where(s => s.CourseId == courseId)
                            .OrderBy(s => s.StartDate)
                            .ToList();
        }
    }
}
