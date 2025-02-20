using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using MOFA.StockManagement.Presentation.UI.Resources;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MOFA.StockManagement.Presentation.UI
{
    public class CustomValidationMetadataProvider : IValidationMetadataProvider
    {
        private const string RESOURCE_KEY_PREFIX = "Validator_";

        public CustomValidationMetadataProvider()
        {
        }

        public void CreateValidationMetadata(ValidationMetadataProviderContext context)
        {
            var metaData = context.ValidationMetadata.ValidatorMetadata;

            if (context.Key.ModelType.GetTypeInfo().IsValueType &&
                !(context.Key.ModelType.IsGenericType && context.Key.ModelType.GetGenericTypeDefinition() == typeof(Nullable<>)) &&
                metaData.Where(m => m.GetType() == typeof(RequiredAttribute)).Count() == 0)
            {
                metaData.Add(new RequiredAttribute());
            }

            foreach (var obj in metaData)
            {
                if (!(obj is ValidationAttribute attr))
                {
                    continue;
                }
                string name = RESOURCE_KEY_PREFIX + attr.GetType().Name;

                string? newMessage = SharedResource.ResourceManager.GetString(name);
                if (string.IsNullOrEmpty(newMessage))
                {
                    continue;
                }

                attr.ErrorMessageResourceType = typeof(SharedResource);
                attr.ErrorMessageResourceName = name;
                attr.ErrorMessage = null;
            }
        }
    }
}
