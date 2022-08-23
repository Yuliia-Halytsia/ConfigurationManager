using System.Collections.Generic;
using ConfigurationManager.FileProcessing;
using FluentAssertions;
using Moq;
using Xunit;

namespace ConfigurationManager.Tests.FileProcessing.FileHierarchyProcessorTests
{
    public partial class FileHierarchyBuilderTests
    {
        public class BuildHierarchy
        {
            [Fact]
            public void Should_Return_File_Hierarchy()
            {
                //Arrange
                var fileName = "Env-1.txt";
                var fileInHierarchy = "Env.txt";
                var path = "Files";
                var expectedFileHierarchy = new List<string> {$"{path}\\Default.txt", $"{path}\\{fileInHierarchy}", $"{path}\\{fileName}" };

                var fileNameIterator = new Mock<IFileNameIterator>();
                fileNameIterator.Setup(f => f.GetFileNames(fileName)).Returns(new[] { fileInHierarchy });
                
                var fileHierarchyBuilder = new FileHierarchyBuilder(fileNameIterator.Object, $"{path}\\{fileName}");

                //Act
                var fileHierarchy = fileHierarchyBuilder.BuildHierarchy();

                //Assert
                fileHierarchy.Should().BeEquivalentTo(expectedFileHierarchy, cfg => cfg.WithStrictOrdering());
            }
        }
    }
}
