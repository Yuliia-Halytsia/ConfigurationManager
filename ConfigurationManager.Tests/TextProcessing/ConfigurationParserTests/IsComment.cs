using ConfigurationManager.TextProcessing;
using FluentAssertions;
using Xunit;

namespace ConfigurationManager.Tests.TextProcessing.ConfigurationParserTests
{
    public partial class ConfigurationParserTests
    {
        public class IsComment
        {
            [Theory]
            [InlineData("#this is comment", true)]
            [InlineData("//comment", true)]
            [InlineData("property=value", false)]
            public void Should_Return_Whether_Line_Is_Comment(string line, bool isComment)
            {
                //Arrange
                var configurationParser = new ConfigurationPropertyParser();

                //Act
                var isCommentResult = configurationParser.IsComment(line);

                //Assert
                isCommentResult.Should().Be(isComment);
            }
        }
    }
}
