using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MOFA.StockManagement.Infrastructure.Models.Extension
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class RequiredIfAttribute : ValidationAttribute
    {
        public RequiredIfAttribute(string otherProperty, object otherPropertyValue, bool equality = true)
            : base("'{0}' is required because '{1}' has a value {3}'{2}'.")
        {

            OtherProperty = otherProperty;
            OtherPropertyValue = otherPropertyValue;
            IsInverted = !equality;
        }

        public string OtherProperty { get; set; }
        public string OtherPropertyDisplayName { get; set; } = string.Empty;
        public object OtherPropertyValue { get; set; }

        public bool IsInverted { get; set; }

        public override bool RequiresValidationContext => true;

        public override string FormatErrorMessage(string name)
        {
            return string.Format(
                CultureInfo.CurrentCulture, ErrorMessageString, name,
                OtherProperty, OtherPropertyValue,
                IsInverted ? "other than " : "of ");
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext == null) throw new ArgumentNullException(nameof(validationContext));

            var otherProperty = validationContext.ObjectType.GetProperty(OtherProperty);
            object? otherValue = null;
            if (otherProperty != null)
                otherValue = otherProperty.GetValue(validationContext.ObjectInstance);
            else if (validationContext.Items.Keys.Contains(OtherProperty))
                otherValue = validationContext.Items[OtherProperty];
            else
            {
                return new ValidationResult(string.Format(CultureInfo.CurrentCulture,
                    "Could not find a property named '{0}'.", OtherProperty));
            }

            if (!IsInverted && Equals(otherValue, OtherPropertyValue) || IsInverted && !Equals(otherValue, OtherPropertyValue))
            {
                if (value is null)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }

                var val = value as string;
                if (val != null && val.Trim().Length == 0)
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
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
