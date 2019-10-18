namespace AspCoreMicroservice.Core.Utilities
{
    public interface IFileManager
    {
        bool FileExists(string filePath);
        string ReadFileContents(string filePath);
    }
}
