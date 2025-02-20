using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MOFA.StockManagement.Infrastructure.Models.Extension
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DataContainsAttribute : ValidationAttribute
    {
        public string Contains { get; set; }
        public string? OtherProperty { get; set; }
        public string OtherPropertyDisplayName { get; set; } = string.Empty;
        public object? OtherPropertyValue { get; set; }
        public bool IsInverted { get; set; }

        public DataContainsAttribute(string contains, string? otherProperty = null, object? otherPropertyValue = null, bool equality = true)
        : base("'{0}' value should be [{1}]")
        {
            Contains = contains;
            OtherProperty = otherProperty;
            OtherPropertyValue = otherPropertyValue;
            IsInverted = !equality;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, Contains.Length < 50 ? string.Join(", ", Contains) : " within allowed values ");
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }

            if (value is null)
            {
                return ValidationResult.Success;
            }
            bool condition = true;
            if (!string.IsNullOrEmpty(OtherProperty) && OtherPropertyValue != null)
            {
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
                condition = !IsInverted && Equals(otherValue, OtherPropertyValue) || IsInverted && !Equals(otherValue, OtherPropertyValue);
            }

            var typeContains = Contains.Split(new char[] { '#', ',' }, StringSplitOptions.TrimEntries).ToArray();
            var isContained = typeContains.Contains(value?.ToString());
            if (!condition)
                return ValidationResult.Success;
            return isContained
                ? ValidationResult.Success
                : new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
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
