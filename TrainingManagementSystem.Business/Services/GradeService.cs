using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManagementSystem.Business.Services.Interfaces;
using TrainingManagementSystem.DataAccess.Models;
using TrainingManagementSystem.DataAccess.Repositories.Interfaces;

namespace TrainingManagementSystem.Business.Services
{
    public class GradeService : IGradeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GradeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Grade> GetAllGrades()
        {
            return _unitOfWork.Grades.GetAll();
        }
        public void DeleteGradeSoft(int id)
        {
            var grade = _unitOfWork.Grades.GetFirstOrDefault(g => g.Id == id);
            if (grade != null)
            {
                _unitOfWork.Grades.SoftDelete(grade);
                _unitOfWork.Save();
            }
        }
    }
}
