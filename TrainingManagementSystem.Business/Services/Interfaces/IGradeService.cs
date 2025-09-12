using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManagementSystem.Business.ViewModels;
using TrainingManagementSystem.DataAccess.Models;

namespace TrainingManagementSystem.Business.Services.Interfaces
{
    public interface IGradeService
    {
        IEnumerable<GradeVM> GetGradesForTrainee(int traineeId, int pageNumber, int pageSize=5);
        public Grade GetGradeById(int id);
        void CreateGrade(GradeVM gradeVM);
        public void UpdateGrade(Grade grade);


        void DeleteGradeSoft(int id); // Soft Delete
        GradeIndexVM GetAllGrades(string searchTerm, int pageNumber, int pageSize = 5);
        GradeVM GetCreateVM();  // return view model with dropdown Lists for sessions, trainees, instructors
        IEnumerable<SelectListItem> GetSessionsDropdown();
        IEnumerable<SelectListItem> GetTraineesDropdown();
        IEnumerable<SelectListItem> GetInstructorsDropdown();
    }
}
