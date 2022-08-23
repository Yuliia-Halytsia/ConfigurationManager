using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConfigurationManager.FileProcessing
{
    public interface IFileNameIterator
    {
        IEnumerable<string> GetFileNames(string targetedFileName);
    }

    public class DashFileNameIterator : IFileNameIterator
    {
        private const string LevelPattern = @"-(\w)+";
        
        public IEnumerable<string> GetFileNames(string fileName)
        {
            while (NextExists(fileName))
            {
                fileName = GetNextFileInHierarchy(fileName);
                yield return fileName;
            }
        }
        
        private string GetNextFileInHierarchy(string fileName)
        {
            var lastLevel = Regex.Matches(fileName, LevelPattern).Last().Value;
            return fileName.Replace(lastLevel, string.Empty);
        }

        private bool NextExists(string fileName)
            => Regex.Matches(fileName, LevelPattern).Any();
    }
}