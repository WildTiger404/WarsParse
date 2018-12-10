using System;
using System.Configuration;
using System.IO;
using ParserService;

namespace FileManager
{
    public class FileManager : IFileManager
    {
        public string GetPathToNewSolution(WarsTask model, string solutionDestinationPath)
        {

            var env = Environment.CurrentDirectory;

            File.WriteAllText(env + @"\Resource\Template\Template\Public\PublicTest.cs", model.PublicTest);
            File.WriteAllText(env + @"\Resource\Template\Template\Internal\HiddenTest.cs", model.PrivateTest);
            File.WriteAllText(env + @"\Resource\Template\Template\Kata.cs", model.Solution);
            File.WriteAllText(env + @"\Resource\Template\Template\Readme.md", model.Description);


            var newSolutionPath = $@"{solutionDestinationPath}\{model.Solution}";
            Directory.CreateDirectory(newSolutionPath);
            return newSolutionPath;
        }


        public void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            foreach (FileInfo file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
            }
        }
    }

}
