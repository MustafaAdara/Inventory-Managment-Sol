using System.ComponentModel.DataAnnotations;

namespace MOFA.StockManagement.Infrastructure.Models.Extension
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValidateItemAttribute : ValidationAttribute
    {
        protected List<ValidationResult> ValidationResults = new();
        protected object? OtherPropertyValue;

        //$"Customer code \"{invoice.CustomerId}\" does not exist."
        public ValidateItemAttribute(string otherProperty, bool required = true)
            : base("{0} \"{1}\" does not exist.")
        {
            OtherProperty = otherProperty;
            Required = required;
        }
        public ValidateItemAttribute(bool required = true)
            : base("{0} \"{1}\" does not exist.")
        {
            Required = required;
        }

        public string? OtherProperty { get; set; }
        public bool Required { get; set; }

        public override string FormatErrorMessage(string name)
        {

            return string.Format(ErrorMessageString, name, OtherPropertyValue);
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext is null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }
            if (value is null && OtherProperty is null && !Required)
                return ValidationResult.Success;

            if (OtherProperty is not null)
            {
                var otherProperty = validationContext.ObjectType.GetProperty(OtherProperty);
                if (otherProperty is not null)
                {
                    OtherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance) ?? default!;
                }
            }
            ValidationResults = new List<ValidationResult>();
            var context = (OtherProperty is not null && OtherPropertyValue is not null) ?
                new ValidationContext(value!, new Dictionary<object, object?>() { { OtherProperty, OtherPropertyValue } }) :
                new ValidationContext(value!);
            var isValid = Validator.TryValidateObject(value!, context, ValidationResults, true);
            if (isValid)
            {
                return ValidationResult.Success;
            }

            var errors = string.Join("#", ValidationResults.Select(s => s.ErrorMessage));
            return new ValidationResult(errors);
        }
    }
}
