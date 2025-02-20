using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MOFA.StockManagement.Infrastructure.Models.Extension
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class FixedLengthIfAttribute : ValidationAttribute
    {
        public FixedLengthIfAttribute(string otherProperty, object otherPropertyValue, int requiredLength)
            : base("The '{0}' must be '{1}' numbers when a '{2}' value equal '{3}'.")

        //The 'Id' must be 'Type' numbers when a value of 'B'.
        {
            OtherProperty = otherProperty;
            OtherPropertyValue = otherPropertyValue;
            RequiredLength = requiredLength;
            IsInverted = false;
        }

        public string OtherProperty { get; set; }
        public string OtherPropertyDisplayName { get; set; } = string.Empty;
        public object OtherPropertyValue { get; set; }

        public int RequiredLength { get; set; }

        public bool IsInverted { get; set; }

        public override bool RequiresValidationContext => true;

        public override string FormatErrorMessage(string name)
        {
            return string.Format(
                CultureInfo.CurrentCulture, ErrorMessageString, name,
                RequiredLength, OtherProperty, OtherPropertyValue,
                IsInverted ? "other than " : "of ");
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext == null) throw new ArgumentNullException(nameof(validationContext));

            var otherProperty = validationContext.ObjectType.GetProperty(OtherProperty);
            if (otherProperty == null)
                return new ValidationResult(string.Format(CultureInfo.CurrentCulture,
                    "Could not find a property named '{0}'.", OtherProperty));

            var otherValue = otherProperty.GetValue(validationContext.ObjectInstance) ?? "";
            if (!IsInverted && Equals(otherValue, OtherPropertyValue) ||
                IsInverted && !Equals(otherValue, OtherPropertyValue))
            {
                if (value is null || (string.IsNullOrEmpty(value?.ToString()) && otherValue.Equals("P")))
                {
                    return ValidationResult.Success;
                }

                if (value?.ToString()?.Length != RequiredLength)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;
        }

        private object _typeId = new object();
        public override object TypeId
        {
            get
            {
                return this._typeId;
            }
        }
    }
}
