using System.ComponentModel.DataAnnotations;

namespace MOFA.StockManagement.Infrastructure.Models.Extension
{
    public class MatchArrayAttribute : ValidationAttribute
    {
        private string _otherPropertyDisplayName = null!;


        public MatchArrayAttribute(string otherProperty)
        : base("{0} and {0} array not consistent.")
        {
            OtherProperty = otherProperty;
        }

        public string OtherProperty { get; set; }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, _otherPropertyDisplayName);
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext is null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }

            var otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);
            var otherValue = otherPropertyInfo?.GetValue(validationContext.ObjectInstance);

            var otherPropertyDisplayName = otherPropertyInfo?.GetCustomAttributes(typeof(DisplayAttribute), true).
                    FirstOrDefault() as DisplayAttribute;
            _otherPropertyDisplayName = otherPropertyDisplayName?.GetName() ?? default!;

            var valueArray = value?.ToString()?.Split(new char[] { ',', '#' }).Length ?? 0;
            var otherPropertyValueArray = otherValue?.ToString()?.Split(new char[] { ',', '#' }).Length ?? 0;

            return valueArray == otherPropertyValueArray
                ? ValidationResult.Success
                : new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
