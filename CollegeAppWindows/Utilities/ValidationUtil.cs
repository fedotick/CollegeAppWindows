using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CollegeAppWindows.Utilities
{
    public static class ValidationUtil
    {
        public static Dictionary<string, List<string>> ValidateModel<T>(T model)
        {
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
            {
                var errors = new Dictionary<string, List<string>>();

                foreach (var validationResult in validationResults)
                {
                    string propertyName = validationResult.MemberNames.FirstOrDefault();
                    string errorMessage = validationResult.ErrorMessage;

                    if (!errors.ContainsKey(propertyName))
                    {
                        errors[propertyName] = new List<string>();
                    }

                    errors[propertyName].Add(errorMessage);
                }

                return errors;
            }

            return new Dictionary<string, List<string>>();
        }
    }
}
