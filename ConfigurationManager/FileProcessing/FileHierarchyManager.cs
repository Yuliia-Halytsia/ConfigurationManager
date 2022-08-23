using System.Collections.Generic;

namespace ConfigurationManager.FileProcessing
{
    public interface IFileHierarchyManager
    {
        Queue<string> GetFilesQueue();
    }

    public class FileHierarchyManager : IFileHierarchyManager
    {
        private readonly IFileHierarchyBuilder _fileHierarchyBuilder;
        private readonly IFileHierarchyProcessor _fileHierarchyProcessor;

        public FileHierarchyManager(
            IFileHierarchyBuilder fileHierarchyBuilder,
            IFileHierarchyProcessor fileHierarchyProcessor)
        {
            _fileHierarchyBuilder = fileHierarchyBuilder;
            _fileHierarchyProcessor = fileHierarchyProcessor;
        }

        public Queue<string> GetFilesQueue()
        {
            var fileHierarchy = _fileHierarchyBuilder.BuildHierarchy();
            return _fileHierarchyProcessor.Process(fileHierarchy);
        }
    }
}
