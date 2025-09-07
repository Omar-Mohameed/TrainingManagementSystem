using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TrainingManagementSystem.Business.CustomValidators
{
    public class NoNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            string input = value.ToString();

            // Regex 
            return !Regex.IsMatch(input, @"\d");
        }
    }
}
