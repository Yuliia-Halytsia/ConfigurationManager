using ConfigurationManager.Models;
using ConfigurationManager.Utils;

namespace ConfigurationManager.PropertiesProcessing
{
    public interface ISemanticValidator
    {
        ConfigurationPropertyResult Validate<T>(ConfigurationTypeProperty property);
    }

    public class SemanticValidator : ISemanticValidator
    {
        public ConfigurationPropertyResult Validate<T>(ConfigurationTypeProperty property)
        {
            if (property.Value.TryConvert(property.Type, out _))
                return new SuccessSemanticValidationResult(property);

            return new FailedSemanticValidationResult($"'{property.Value}' value is invalid for '{property.FullName}' property");
        }
    }
}
