using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentValidation;

namespace ConfigurationManager.FileProcessing
{
    public interface IFileHierarchyProcessor
    {
        Queue<string> Process(IList<string> files);
    }

    public class FileHierarchyProcessor : IFileHierarchyProcessor
    {
        private readonly IValidator<IList<string>> _fileHierarchyValidator;

        public FileHierarchyProcessor(IValidator<IList<string>> fileHierarchyValidator)
        {
            _fileHierarchyValidator = fileHierarchyValidator;
        }

        public Queue<string> Process(IList<string> files)
        {
           var validationResult = _fileHierarchyValidator.Validate(files);

           if(!validationResult.IsValid)
               throw new Exception($"following rules violated: {string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))}");

           var filesQueue = new Queue<string>();
           foreach (var file in files)
           {
               if(File.Exists(file))
                   filesQueue.Enqueue(file);
           }

           return filesQueue;
        }
    }
}
