using System.Collections.Generic;
using System.IO;
using ConfigurationManager.Models;

namespace ConfigurationManager.TextProcessing
{
    public interface IFileConfigurationProcessor
    {
        IAsyncEnumerable<ConfigurationPropertyResult> ParseConfigFileAsync(string fileName);
    }

    public class FileConfigurationProcessor : IFileConfigurationProcessor
    {
        private readonly IConfigurationPropertyParser _configurationParser;

        public FileConfigurationProcessor(IConfigurationPropertyParser configurationParser)
        {
            _configurationParser = configurationParser;
        }

        public async IAsyncEnumerable<ConfigurationPropertyResult> ParseConfigFileAsync(string fileName)
        {
            var lineIndex = 0;

            using var streamReader = new StreamReader(fileName);
            while (!streamReader.EndOfStream)
            {
                var line = await streamReader.ReadLineAsync();
                lineIndex++;

                if(_configurationParser.IsComment(line))
                    continue;

                if(_configurationParser.TryParse(line, out ConfigurationProperty property))
                    yield return new SuccessParsingResult(property);
                else
                    yield return new FailedParsingResult($"error in {fileName} file: invalid line at {lineIndex}");
            }
        }
    }
}