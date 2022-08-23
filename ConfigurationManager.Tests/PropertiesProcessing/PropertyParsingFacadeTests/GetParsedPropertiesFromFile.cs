using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConfigurationManager.Models;
using ConfigurationManager.PropertiesProcessing;
using ConfigurationManager.Tests.TestEntities;
using ConfigurationManager.TextProcessing;
using FluentAssertions;
using Moq;
using Xunit;

namespace ConfigurationManager.Tests.PropertiesProcessing.PropertyParsingFacadeTests
{
    public partial class PropertyParsingFacadeTests
    {
        public class GetParsedPropertiesFromFile
        {
            [Fact]
            public async Task Should_Handle_Failed_Parsing_Result()
            {
                //Arrange
                var failedParsingResult = new FailedParsingResult("failed parsing result");

                var fileConfigurationProcessor = new Mock<IFileConfigurationProcessor>();
                var parsingErrorHandlingStrategy = new Mock<IParsingErrorHandlingStrategy>();

                var parsingResults = new TestAsyncEnumerable<ConfigurationPropertyResult>(new[] {failedParsingResult });
                fileConfigurationProcessor.Setup(p => p.ParseConfigFileAsync(It.IsAny<string>()))
                                 .Returns(parsingResults);

                var filePropertyParsingFacade = new FilePropertyParsingFacade(
                    fileConfigurationProcessor.Object,
                    parsingErrorHandlingStrategy.Object);

                //Act
                var parsedProperties = filePropertyParsingFacade.GetParsedPropertiesFromFile("fileName");
                await foreach (var property in parsedProperties) { }

                //Assert
                parsingErrorHandlingStrategy.Verify(s => s.HandleFailedParsingResult(failedParsingResult), Times.Once);
            }

            [Fact]
            public async Task Should_Return_Configuration_Properties_If_No_Errors()
            {
                var configurationProperty = new ConfigurationTypeProperty
                { ClassName = "Class", PropertyName = "Property", Value = "Value", Type = typeof(string) };
                var successParsingResult = new SuccessParsingResult(configurationProperty);

                var fileConfigurationProcessor = new Mock<IFileConfigurationProcessor>();
                var parsingErrorHandlingStrategy = new Mock<IParsingErrorHandlingStrategy>();

                var parsingResults = new TestAsyncEnumerable<ConfigurationPropertyResult>(new[] { successParsingResult });
                fileConfigurationProcessor.Setup(p => p.ParseConfigFileAsync(It.IsAny<string>()))
                    .Returns(parsingResults);

                var filePropertyParsingFacade = new FilePropertyParsingFacade(
                    fileConfigurationProcessor.Object,
                    parsingErrorHandlingStrategy.Object);

                //Act
                var validatedPropertiesIterator = filePropertyParsingFacade.GetParsedPropertiesFromFile("fileName");
                var actualPropCollection = new List<ConfigurationProperty>();
                await foreach (var property in validatedPropertiesIterator) { actualPropCollection.Add(property); }

                //Assert
                actualPropCollection.Should().BeEquivalentTo(configurationProperty);
            }
        }
    }
}
