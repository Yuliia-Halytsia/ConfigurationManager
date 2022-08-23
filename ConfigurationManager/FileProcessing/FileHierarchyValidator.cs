using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentValidation;

namespace ConfigurationManager.FileProcessing
{
    public class FileHierarchyValidator : AbstractValidator<IList<string>>
    {
        public FileHierarchyValidator()
        {
            RuleFor(f => f.First())
                .Must(filePath => filePath.EndsWith("Default.txt"))
                .WithMessage("first file in hierarchy should be Default.txt");

            RuleFor(f => f.First())
                .Must(File.Exists)
                .WithMessage("Default.txt is mandatory");

            RuleFor(f => f.Last())
                .Must(File.Exists)
                .WithMessage("Targeted file is mandatory");
        }
    }
}
