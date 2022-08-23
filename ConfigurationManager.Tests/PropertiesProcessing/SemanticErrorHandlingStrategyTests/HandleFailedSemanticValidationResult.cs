using System;
using ConfigurationManager.Models;
using ConfigurationManager.PropertiesProcessing;
using FluentAssertions;
using Xunit;

namespace ConfigurationManager.Tests.PropertiesProcessing.SemanticErrorHandlingStrategyTests
{
    public partial class SemanticErrorHandlingStrategyTests
    {
        public class HandleFailedSemanticValidationResult
        {
            [Fact]
            public void Should_Throw_Exception_On_Failed_Parsing_Result()
            {
                //Arrange
                var exceptionOnParsingErrorStrategy = new ExceptionOnSemanticErrorStrategy();
                var failedSemanticResult = new FailedSemanticValidationResult("failed semantic validation result");

                //Act
                Action handleAction = () => exceptionOnParsingErrorStrategy.HandleFailedSemanticValidationResult(failedSemanticResult);

                //Assert
                handleAction.Should().Throw<Exception>().WithMessage(failedSemanticResult.ValidationError.Message);
            }
        }
    }
}
