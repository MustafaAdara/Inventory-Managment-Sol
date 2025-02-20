using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MOFA.StockManagement.Infrastructure.Models.Extension
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class NotEqualIfDecimalAttribute : ValidationAttribute
    {
        public NotEqualIfDecimalAttribute(string otherProperty, string valueNotEqual)
            : base("The '{0}' must not equal '{1}' numbers when a '{2}' value equal '{3}'.")

        //The 'Id' must be 'Type' numbers when a value of 'B'.
        {
            OtherProperty = otherProperty;
            ValueNotEqual = valueNotEqual;
            IsInverted = false;
        }

        public string OtherProperty { get; set; }
        public string ValueNotEqual { get; set; }

        public bool IsInverted { get; set; }

        public override bool RequiresValidationContext => true;

        public override string FormatErrorMessage(string name)
        {
            return string.Format(
                CultureInfo.CurrentCulture, ErrorMessageString, name,
                ValueNotEqual, OtherProperty,
                IsInverted ? "other than " : "of ");
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext == null) throw new ArgumentNullException(nameof(validationContext));

            var otherProperty = validationContext.ObjectType.GetProperty(OtherProperty);
            if (otherProperty == null)
                return new ValidationResult(string.Format(CultureInfo.CurrentCulture,
                    "Could not find a property named '{0}'.", OtherProperty));

            var otherValue = otherProperty.GetValue(validationContext.ObjectInstance);
            if (otherValue is not null || !Equals(otherValue, 0))
            {
                if (value is not null && !Equals(value, ValueNotEqual))
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
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
