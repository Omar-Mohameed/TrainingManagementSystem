using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using TrainingManagementSystem.DataAccess.Models;

namespace TrainingManagementSystem.DataAccess.Repositories.Interfaces
{
    public interface IGradeRepository : IGenericRepository<Grade>
    {
        //View grades per trainee(with pagination).
        IEnumerable<Grade> GetGradesByTrainee(int traineeId, int page, int pageSize, out int totalCount);
        // Check if grade already exists for this Trainee & Session
        bool GradeExists(int traineeId, int sessionId);
    }
}
