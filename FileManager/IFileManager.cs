using System.IO;
using ParserService;

namespace FileManager
{
    public interface IFileManager
    {
        void UpdateResourceFile(WarsTask model);
        void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target);
    }
}