using System;
using System.Collections.Generic;
using System.Linq;
using ConfigurationManager.FileProcessing;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace ConfigurationManager.Tests.FileProcessing.FileHierarchyProcessorTests
{
    public partial class FileHierarchyProcessorTests
    {
        public class Process
        {
            [Fact]
            public void Should_Throw_Exception_If_Any_Validation_Errors()
            {
                //Arrange
                var filesHierarchy = new List<string>();
                var fileHierarchyValidator = new Mock<IValidator<IList<string>>>();
                var validationErrors = new[]
                {
                    new ValidationFailure("property 1", "error message 1"),
                    new ValidationFailure("property 2", "error message 2")
                };
                fileHierarchyValidator.Setup(v => v.Validate(It.IsAny<IList<string>>()))
                    .Returns(new ValidationResult(validationErrors));
                var fileHierarchyProcessor = new FileHierarchyProcessor(fileHierarchyValidator.Object);

                //Act
                Action processAction = () => fileHierarchyProcessor.Process(filesHierarchy);

                //Assert
                processAction.Should().Throw<Exception>().WithMessage("following rules violated: error message 1, error message 2");
            }

            [Fact]
            public void Should_Skip_Non_Existing_Files()
            {
                //Arrange
                var filesHierarchy = new List<string> {"TestFiles\\Default.txt", "TestFiles\\Env-1.txt", "TestFiles\\Env-1-2.txt" };
                var fileHierarchyValidator = new Mock<IValidator<IList<string>>>();
                fileHierarchyValidator.Setup(v => v.Validate(It.IsAny<IList<string>>()))
                    .Returns(new ValidationResult());
                var fileHierarchyProcessor = new FileHierarchyProcessor(fileHierarchyValidator.Object);

                var expectedQueue = new Queue<string>(filesHierarchy.Take(2).ToArray());

                //Act
                var filesQueue = fileHierarchyProcessor.Process(filesHierarchy);

                //Assert
                filesQueue.Should().BeEquivalentTo(expectedQueue);
            }
        }
    }
    
}
