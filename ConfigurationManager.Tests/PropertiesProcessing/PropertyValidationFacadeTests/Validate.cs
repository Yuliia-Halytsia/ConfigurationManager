using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationManager.Models;
using ConfigurationManager.PropertiesProcessing;
using ConfigurationManager.Tests.TestEntities;
using FluentAssertions;
using Moq;
using Xunit;

namespace ConfigurationManager.Tests.PropertiesProcessing.PropertyValidationFacadeTests
{
    public partial class PropertyValidationFacadeTests
    {
        public class Validate
        {
            [Fact]
            public async Task Should_Handle_Failed_Semantic_Validation_Result()
            {
                //Arrange
                var failedSemanticResult = new FailedSemanticValidationResult("failed semantic result");

                var semanticValidator = new Mock<ISemanticValidator>();
                var semanticErrorHandlingStrategy = new Mock<ISemanticErrorHandlingStrategy>();

                var properties = new TestAsyncEnumerable<ConfigurationTypeProperty>(new[] {new ConfigurationTypeProperty()});
                semanticValidator.Setup(p => p.Validate<It.IsAnyType>(It.IsAny<ConfigurationTypeProperty>()))
                                 .Returns(failedSemanticResult);

                var propertyValidationFacade = new PropertyValidationFacade(
                    semanticValidator.Object,
                    semanticErrorHandlingStrategy.Object);

                //Act
                var validatedProperties = propertyValidationFacade.Validate<CustomType>(properties);
                await foreach (var property in validatedProperties) { }


                //Assert
                semanticErrorHandlingStrategy.Verify(s => s.HandleFailedSemanticValidationResult(failedSemanticResult), Times.Once);
            }

            [Fact]
            public async Task Should_Return_Configuration_Properties_If_No_Errors()
            {
                var configurationProperty = new ConfigurationTypeProperty
                    { ClassName = "Class", PropertyName = "Property", Value = "Value", Type = typeof(string)};
                var successSemanticResult = new SuccessSemanticValidationResult(configurationProperty);

                var semanticValidator = new Mock<ISemanticValidator>();
                var semanticErrorHandlingStrategy = new Mock<ISemanticErrorHandlingStrategy>();

                var properties = new List<ConfigurationTypeProperty> {configurationProperty};
                var asyncEnumerableProperties = new TestAsyncEnumerable<ConfigurationTypeProperty>(new[] { configurationProperty });

                semanticValidator.Setup(p => p.Validate<It.IsAnyType>(It.IsAny<ConfigurationTypeProperty>()))
                                 .Returns(successSemanticResult);

                var propertyValidationFacade = new PropertyValidationFacade(
                    semanticValidator.Object,
                    semanticErrorHandlingStrategy.Object);

                //Act
                var validatedPropertiesIterator = propertyValidationFacade.Validate<CustomType>(asyncEnumerableProperties);
                var actualPropCollection = new List<ConfigurationProperty>();
                await foreach (var property in validatedPropertiesIterator) { actualPropCollection.Add(property); }

                //Assert
                actualPropCollection.Should().BeEquivalentTo(properties);
            }
        }
    }
}
