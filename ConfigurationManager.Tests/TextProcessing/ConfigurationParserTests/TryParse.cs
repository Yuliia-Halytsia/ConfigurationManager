using System.Collections.Generic;
using ConfigurationManager.Models;
using ConfigurationManager.TextProcessing;
using FluentAssertions;
using Xunit;

namespace ConfigurationManager.Tests.TextProcessing.ConfigurationParserTests
{
    public partial class ConfigurationParserTests
    {
        public class TryParse
        {
            [Theory]
            [InlineData("namespace.class.property=value")]
            [InlineData("namespace.class2.property2=value")]
            [InlineData("namespace.class2.property2=value123=;")]
            public void Should_Return_True_If_Line_Can_Be_Parsed(string line)
            {
                //Arrange
                var configurationParser = new ConfigurationPropertyParser();
                var validationResult = configurationParser.TryParse(line, out ConfigurationProperty property);

                //Act
                validationResult.Should().BeTrue();
            }

            [Theory]
            [InlineData("=")]
            [InlineData("property")]
            [InlineData("property=")]
            [InlineData("property==")]
            [InlineData(".property=value")]
            [InlineData("property=value")]
            [InlineData("123property=value")]
            [InlineData("property:value")]
            [InlineData("namespace;class;property=value")]
            [InlineData("namespace.class.1property=value")]
            public void Should_Return_False_If_Line_Can_Not_Be_Parsed(string line)
            {
                //Arrange
                var configurationParser = new ConfigurationPropertyParser();
                var validationResult = configurationParser.TryParse(line, out ConfigurationProperty property);

                //Act
                validationResult.Should().BeFalse();
                property.Should().BeNull();
            }

            [Theory]
            [MemberData(nameof(GetConfigurationPropertiesFromLines))]
            public void Should_Parse_Line_To_Configuration_Property(string line, ConfigurationProperty configurationProperty)
            {
                //Arrange
                var configurationParser = new ConfigurationPropertyParser();

                //Act
                configurationParser.TryParse(line, out ConfigurationProperty actualConfigurationProperty);

                //Assert
                actualConfigurationProperty.Should().BeEquivalentTo(configurationProperty);
            }

            public static IEnumerable<object[]> GetConfigurationPropertiesFromLines()
            {
                yield return new object[] { "namespace.class.property=value123=;", new ConfigurationProperty { Namespace = "namespace", ClassName = "class", PropertyName = "property", Value = "value123=;" } };
                yield return new object[] { "class.property=value1/value2", new ConfigurationProperty { Namespace = "", ClassName = "class", PropertyName = "property", Value = "value1/value2" } };
            }
        }
    }
}

