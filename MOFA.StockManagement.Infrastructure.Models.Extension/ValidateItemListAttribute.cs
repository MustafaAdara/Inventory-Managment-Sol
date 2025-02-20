using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace MOFA.StockManagement.Infrastructure.Models.Extension
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValidateItemListAttribute : ValidationAttribute
    {
        protected List<ValidationResult> ValidationResults = new();


        public ValidateItemListAttribute(string errorMessageForEmptyOrNull = "", bool required = true)
        {
            ErrorMessageForEmptyOrNull = errorMessageForEmptyOrNull;
            Required = required;
        }

        public string ErrorMessageForEmptyOrNull { get; set; }
        public bool Required { get; set; }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageForEmptyOrNull, name);
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext is null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }

            if (value is null)
            {
                if (Required)
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                else
                    return ValidationResult.Success;
            }

            if (!(value is IList list))
            {
                if (Required)
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                else
                    return ValidationResult.Success;
            }

            ValidationResults = new List<ValidationResult>();
            var isValid = true;
            for (var index = 0; index < list?.Count; index++)
            {
                var item = list[index];
                ICollection<ValidationResult> validationResults = new List<ValidationResult>();
                var context = new ValidationContext(item ?? default!);

                var isItemValid = Validator.TryValidateObject(item ?? default!, context, validationResults, true);
                if (!isItemValid)
                {
                    validationResults = validationResults
                        .ToList()
                        .Select(s =>
                        {
                            s.ErrorMessage = $"{validationContext.DisplayName}[{index}].{s.ErrorMessage}";

                            return s;
                        })
                        .ToList();

                    ValidationResults.AddRange(validationResults);
                }

                isValid &= isItemValid;
            }

            if (isValid)
            {
                return ValidationResult.Success;
            }

            var errors = string.Join("#", ValidationResults.Select(s => s.ErrorMessage));
            return new ValidationResult(errors);
        }
    }
}
