using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManagementSystem.DataAccess.Models;

namespace TrainingManagementSystem.Business.Services.Interfaces
{
    public interface IGradeService
    {
        void DeleteGradeSoft(int id); // Soft Delete
        IEnumerable<Grade> GetAllGrades();
    }
}
