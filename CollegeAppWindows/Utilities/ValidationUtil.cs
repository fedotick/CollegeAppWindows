using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        public static List<string> ValidateProperty<T>(T model, string propertyName)
        {
            var propertyInfo = typeof(T).GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property {propertyName} does not exist on type {typeof(T).Name}");
            }

            var validationContext = new ValidationContext(model) { MemberName = propertyName };
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateProperty(propertyInfo.GetValue(model), validationContext, validationResults);

            return validationResults.Select(result => result.ErrorMessage).ToList();
        }
    }
}
