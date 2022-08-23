using System.Collections.Generic;
using System.Linq;

namespace ConfigurationManager.FileProcessing
{
    public interface IFileHierarchyBuilder
    {
        IList<string> BuildHierarchy();
    }

    public class FileHierarchyBuilder : IFileHierarchyBuilder
    {
        private readonly IFileNameIterator _fileNameIterator;
        private readonly string _configurationFilePath;
        private const string DefaultFile = "Default.txt";

        private string ConfigurationFilesDirectory => _configurationFilePath.Split("\\").First();
       
        public FileHierarchyBuilder(
            IFileNameIterator fileNameIterator,
            string filePath)
        {
            _configurationFilePath = filePath;
            _fileNameIterator = fileNameIterator;
        }

        public IList<string> BuildHierarchy()
        {
            var fileName = _configurationFilePath.Split("\\").Last();

            var filesList = new List<string> { fileName };
            filesList.AddRange(_fileNameIterator.GetFileNames(fileName));
            filesList.Add(DefaultFile);

            filesList.Reverse();
            return filesList.Select(GetFilePath).ToList();
        }

        private string GetFilePath(string fileName)
            => $"{ConfigurationFilesDirectory}\\{fileName}";
    }
}
