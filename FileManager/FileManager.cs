using System;
using System.IO;
using ParserService;

namespace FileManager
{
    public class FileManager : IFileManager
    {
        public void UpdateResourceFile(WarsTask model)
        {
            var env = Environment.CurrentDirectory;

            File.WriteAllText(env + @"\Resource\Public\PublicTest.cs", model.PublicTest);
            File.WriteAllText(env + @"\Resource\Internal\HiddenTest.cs", model.PrivateTest);
            File.WriteAllText(env + @"\Resource\Kata.cs", model.Solution);
            File.WriteAllText(env + @"\Resource\Readme.md", model.Description);
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
