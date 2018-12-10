using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ParserService;
using DependencyResolver;
using FileManager;
using Microsoft.Win32.SafeHandles;
using SimpleInjector;
using System.Configuration;
using ProjectBuilder;

namespace WarsParser
{
    class Program
    {
        static void Main(string[] args)
        {

            var container = new Container();
            Resolver.Resolve(container);
            var service = container.GetInstance<IParserService>();
            service.Authorization();
            var task = service.GetTask("https://www.codewars.com/kata/vowel-count");
            Console.WriteLine(task.Name);
            Console.WriteLine(task.PrivateTest);
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine(task.PublicTest);
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine(task.Description);
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine(task.Solution);

            var fileManager = container.GetInstance<IFileManager>();

            var path = fileManager.GetPathToNewSolution(task, ConfigurationManager.AppSettings["solutionsPath"]);

            fileManager.CopyFilesRecursively(new DirectoryInfo(Environment.CurrentDirectory + @"\Resource\Template"),new DirectoryInfo(path));

            //var builder = container.GetInstance<IBuildService>();
            //var flag = builder.Build(
            //    @"E:\Projects\git\labs\padawans-task\Basics\Alghoritms\TheLargestNumberFromDigits\TheLargestNumberFromDigits.sln",
            //    @"E:\Projects\git\labs\padawans-task\Basics\Alghoritms\TheLargestNumberFromDigits\TheLargestNumberFromDigits\bin\Debug");
            //Console.WriteLine(flag);
            Console.ReadLine();
        }
    }
}
