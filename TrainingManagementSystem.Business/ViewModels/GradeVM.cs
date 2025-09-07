using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManagementSystem.Business.ViewModels
{
    public class GradeVM
    {
        public int Id { get; set; }
        public string TraineeName { get; set; }
        public string SessionName { get; set; }
        public double Value { get; set; }
    }

    /*
     id   |   Trainee Name     |    Session   | Grade
      1   |        Omar        |      C#          50
      2   |     ahmed          |     c           60
     */
}
