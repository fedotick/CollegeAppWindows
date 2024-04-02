using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CollegeAppWindows.Utilities
{
    internal class Validation
    {
        public static List<string> ValidateModel<T>(T model)
        {
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
            {

                var errors = new List<string>();

                foreach (var validationResult in validationResults)
                {
                    errors.Add(validationResult.ErrorMessage);
                }
                return errors;
            }

            return new List<string>();
        }
    }
}
