using System;
using ConfigurationManager.Models;

namespace ConfigurationManager.PropertiesProcessing
{
    public interface IParsingErrorHandlingStrategy
    {
        void HandleFailedParsingResult(FailedParsingResult parsingResult);
    }

    public class ExceptionOnParsingErrorStrategy : IParsingErrorHandlingStrategy
    {
        public void HandleFailedParsingResult(FailedParsingResult parsingResult)
        {
            throw new Exception(parsingResult.ValidationError.Message);
        }
    }
}
