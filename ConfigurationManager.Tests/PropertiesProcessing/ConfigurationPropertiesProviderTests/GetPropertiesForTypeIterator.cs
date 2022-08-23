using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationManager.FileProcessing;
using ConfigurationManager.Models;
using ConfigurationManager.PropertiesProcessing;
using ConfigurationManager.Tests.TestEntities;
using ConfigurationManager.TextProcessing;
using FluentAssertions;
using Moq;
using Xunit;

namespace ConfigurationManager.Tests.PropertiesProcessing.ConfigurationPropertiesProviderTests
{
    public partial class ConfigurationPropertiesProviderTests
    {
        public class GetPropertiesForTypeIterator
        {
            [Fact]
            public async Task Should_Skip_Properties_That_Are_Not_Eligible()
            {
                //Arrange
                var fileManager = new Mock<IFileHierarchyManager>();
                var filePropertyParsingFacade = new Mock<IFilePropertyParsingFacade>();
                var propertyValidationFacade = new Mock<IPropertyValidationFacade>();

                var eligibleProperties = new[]
                {

                    new TypeMemberInfo("Prop1", typeof(string)),
                    new TypeMemberInfo("Prop2", typeof(string)),
                    new TypeMemberInfo("Prop", typeof(int))
                };

                var configPropProvider = new FileConfigurationPropertiesProvider(
                    fileManager.Object,
                    filePropertyParsingFacade.Object,
                    propertyValidationFacade.Object);

                var asyncEnumerableProperties = new TestAsyncEnumerable<ConfigurationProperty>(new[]
                {
                    new ConfigurationProperty {PropertyName = "Prop", Value = "1"},
                    new ConfigurationProperty {PropertyName = "NonExistingProp", Value = "value"}
                });

                var expectedPropCollection = new List<ConfigurationTypeProperty>
                {
                    new ConfigurationTypeProperty {PropertyName = "Prop", Value = "1", Type = typeof(int)}
                };

                //Act
                var actualPropIterator = configPropProvider.GetPropertiesForTypeIterator<CustomType>(asyncEnumerableProperties, eligibleProperties);
                var actualPropCollection = new List<ConfigurationTypeProperty>();
                await foreach (var property in actualPropIterator) { actualPropCollection.Add(property); }

                //Assert
                actualPropCollection.ToList().Should().BeEquivalentTo(expectedPropCollection);
            }
        }
    }
    
}
