using System;
using System.IO;
using ParserService;
using DependencyResolver;
using FileManager;
using SimpleInjector;
using System.Configuration;

namespace WarsParser
{
    class Program
    {
        static void Main(string[] args)
        {

            var container = new Container();
            Resolver.Resolve(container);

            var fileManager = container.GetInstance<IFileManager>();
            var service = container.GetInstance<IParserService>();
            service.Authorization();

            var flag = true;

            //Simple case to use this project
            while (flag)
            {
                Console.WriteLine("Enter url:");

                var url = Console.ReadLine();
                
                var task = service.GetTask(url);

                var path = fileManager.GetPathToNewSolution(task, ConfigurationManager.AppSettings["solutionsPath"]);

                fileManager.CopyFilesRecursively(new DirectoryInfo(Environment.CurrentDirectory + @"\Resource\Template"), new DirectoryInfo(path));
                Console.WriteLine("Solution was created successfully");
                Console.WriteLine("Enter N to exit");
                if (Console.ReadLine() == "N")
                    flag = false;
            }

            



            //var task = service.GetTask("https://www.codewars.com/kata/vowel-count");
            //Console.WriteLine(task.Name);
            //Console.WriteLine(task.PrivateTest);
            //Console.WriteLine("------------------------------------------");
            //Console.WriteLine("------------------------------------------");
            //Console.WriteLine("------------------------------------------");
            //Console.WriteLine(task.PublicTest);
            //Console.WriteLine("------------------------------------------");
            //Console.WriteLine("------------------------------------------");
            //Console.WriteLine("------------------------------------------");
            //Console.WriteLine(task.Description);
            //Console.WriteLine("------------------------------------------");
            //Console.WriteLine("------------------------------------------");
            //Console.WriteLine("------------------------------------------");
            //Console.WriteLine(task.Solution);

            Console.ReadLine();
        }
    }
}
