using System.IO;
using ParserService;

namespace FileManager
{
    public interface IFileManager
    {
        string GetPathToNewSolution(WarsTask model, string solutionDestinationPath);
        void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target);
    }
}