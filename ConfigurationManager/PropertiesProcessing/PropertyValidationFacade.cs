using System.Collections.Generic;
using ConfigurationManager.Models;

namespace ConfigurationManager.PropertiesProcessing
{
    public interface IPropertyValidationFacade
    {
        IAsyncEnumerable<ConfigurationProperty> Validate<T>(IAsyncEnumerable<ConfigurationTypeProperty> properties);
    }

    public class PropertyValidationFacade : IPropertyValidationFacade
    {
        private readonly ISemanticValidator _semanticValidator;
        private readonly ISemanticErrorHandlingStrategy _semanticErrorHandlingStrategy;

        public PropertyValidationFacade(
            ISemanticValidator semanticValidator,
            ISemanticErrorHandlingStrategy semanticErrorHandlingStrategy)
        {
            _semanticValidator = semanticValidator;
            _semanticErrorHandlingStrategy = semanticErrorHandlingStrategy;
        }

        public async IAsyncEnumerable<ConfigurationProperty> Validate<T>(IAsyncEnumerable<ConfigurationTypeProperty> properties)
        {
            await foreach (var configurationProperty in properties)
            {
                var validationResult = _semanticValidator.Validate<T>(configurationProperty);

                if (validationResult is FailedSemanticValidationResult result)
                    _semanticErrorHandlingStrategy.HandleFailedSemanticValidationResult(result);
                else
                    yield return validationResult.ConfigurationProperty;
            }
           
        }
    }
}
