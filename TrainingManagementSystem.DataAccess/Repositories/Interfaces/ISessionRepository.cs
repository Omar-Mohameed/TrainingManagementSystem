using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManagementSystem.DataAccess.Models;

namespace TrainingManagementSystem.DataAccess.Repositories.Interfaces
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        void Update(Session session);

        IEnumerable<Session> GetUpcomingSessions();
        IEnumerable<Session> GetSessionsByCourseId(int courseId);
    }
}
