using ConfigurationManager.Models;
using ConfigurationManager.PropertiesProcessing;
using ConfigurationManager.Tests.TestEntities;
using FluentAssertions;
using Xunit;

namespace ConfigurationManager.Tests.PropertiesProcessing.SemanticValidatorTests
{
    public partial class SemanticValidatorTests
    {
        public class Validate
        {
            [Fact]
            public void Should_Return_Success_Result_If_Property_Value_Is_Valid()
            {
                //Arrange
                var property = new ConfigurationTypeProperty { PropertyName = "Prop1", Value = "string", Type = typeof(string) };
                var expectedValidationResult = new SuccessSemanticValidationResult(property);
                var semanticValidator = new SemanticValidator();

                //Act
                var validationResult = semanticValidator.Validate<CustomType>(property);

                //Assert
                validationResult.Should().BeEquivalentTo(expectedValidationResult);
            }

            [Fact]
            public void Should_Return_Failed_Result_If_Property_Value_Not_Valid()
            {
                //Arrange
                var property = new ConfigurationTypeProperty {PropertyName = "Prop", Value = "string", Type = typeof(int)};
                var expectedValidationResult = new FailedSemanticValidationResult($"'{property.Value}' value is invalid for '{property.FullName}' property");
                var semanticValidator = new SemanticValidator();

                //Act
                var validationResult = semanticValidator.Validate<CustomType>(property);

                //Assert
                validationResult.Should().BeEquivalentTo(expectedValidationResult);
            }
        }
    }

}
