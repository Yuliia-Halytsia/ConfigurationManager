using System;
using ConfigurationManager.Models;
using ConfigurationManager.PropertiesProcessing;
using FluentAssertions;
using Xunit;

namespace ConfigurationManager.Tests.PropertiesProcessing.ParsingErrorHandlingStrategyTests
{
    public partial class ParsingErrorHandlingStrategyTests
    {
        public class HandleFailedParsingResult
        {
            [Fact]
            public void Should_Throw_Exception_On_Failed_Parsing_Result()
            {
                //Arrange
                var exceptionOnParsingErrorStrategy = new ExceptionOnParsingErrorStrategy();
                var failedParsingResult = new FailedParsingResult("failed parsing result");

                //Act
                Action handleAction = () => exceptionOnParsingErrorStrategy.HandleFailedParsingResult(failedParsingResult);

                //Assert
                handleAction.Should().Throw<Exception>().WithMessage(failedParsingResult.ValidationError.Message);
            }
        }
    }
}
