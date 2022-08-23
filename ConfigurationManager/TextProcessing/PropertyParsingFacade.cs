using System.Collections.Generic;
using ConfigurationManager.Models;
using ConfigurationManager.PropertiesProcessing;

namespace ConfigurationManager.TextProcessing
{
    public interface IFilePropertyParsingFacade
    {
        IAsyncEnumerable<ConfigurationProperty> GetParsedPropertiesFromFile(string fileName);
    }

    public class FilePropertyParsingFacade : IFilePropertyParsingFacade
    {
        private readonly IFileConfigurationProcessor _fileConfigurationProcessor;
        private readonly IParsingErrorHandlingStrategy _parsingErrorHandlingStrategy;

        public FilePropertyParsingFacade(
            IFileConfigurationProcessor fileConfigurationProcessor,
            IParsingErrorHandlingStrategy parsingErrorHandlingStrategy)
        {
            _fileConfigurationProcessor = fileConfigurationProcessor;
            _parsingErrorHandlingStrategy = parsingErrorHandlingStrategy;
        }

        public async IAsyncEnumerable<ConfigurationProperty> GetParsedPropertiesFromFile(string fileName)
        {
            var propParsingResults = _fileConfigurationProcessor.ParseConfigFileAsync(fileName);
            await foreach (var parsingResult in propParsingResults)
            {
                if (!parsingResult.IsValid)
                    _parsingErrorHandlingStrategy.HandleFailedParsingResult(parsingResult as FailedParsingResult);
                else
                    yield return parsingResult.ConfigurationProperty;
            }
        }
    }
}
