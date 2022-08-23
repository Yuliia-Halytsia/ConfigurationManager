using ConfigurationManager.FileProcessing;
using FluentAssertions;
using Xunit;

namespace ConfigurationManager.Tests.FileProcessing.FileNameTemplateProviderTests
{
    public partial class FileNameIteratorTests
    {
        public class GetFileNames
        {
            [Fact]
            public void Should_Return_File_Names()
            {
                //Arrange
                var fileNameIterator = new DashFileNameIterator();
                var fileName = "env-1-2-3.txt";
                var expectedFiles = new[] {"env-1-2.txt", "env-1.txt", "env.txt"};
                
                //Act
                var actualFiles = fileNameIterator.GetFileNames(fileName);

                //Assert
                actualFiles.Should().BeEquivalentTo(expectedFiles);
            }
        }
    }
}
