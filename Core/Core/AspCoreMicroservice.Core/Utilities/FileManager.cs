using Microsoft.Extensions.Caching.Distributed;
using System.IO;
using System.Text.RegularExpressions;

namespace AspCoreMicroservice.Core.Utilities
{
    public class FileManager : IFileManager
    {
        private readonly IDistributedCache _cache;
        public FileManager(IDistributedCache cache)
        {
            _cache = cache;
        }
        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }
        public string ReadFileContents(string filePath)
        {
            if (_cache.GetString(filePath) != null) return (string)_cache.GetString(filePath);
            string stringData;
            using (var schameReader = File.OpenText(filePath)) stringData = schameReader.ReadToEnd();
            var fileContent = Regex.Replace(stringData, @"\\", @"\\");
            _cache.SetString(filePath, fileContent);
            return fileContent;
        }
    }
}
