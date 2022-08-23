using System;
using ConfigurationManager.Models;

namespace ConfigurationManager.PropertiesProcessing
{
    public interface ISemanticErrorHandlingStrategy
    {
        void HandleFailedSemanticValidationResult(FailedSemanticValidationResult validationResult);
    }

    public class ExceptionOnSemanticErrorStrategy : ISemanticErrorHandlingStrategy
    {
        public void HandleFailedSemanticValidationResult(FailedSemanticValidationResult validationResult)
        {
            throw new Exception(validationResult.ValidationError.Message);
        }
    }
}
