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
using ProjectBuilder;
using System.Configuration;

namespace WarsParser
{
    class Program
    {
        static void Main(string[] args)
        {

            var container = new Container();
            Resolver.Resolve(container);
            var service = container.GetInstance<IParserService>();
            var fileManager = container.GetInstance<IFileManager>();



            service.Authorization();
            var task = service.GetTask("https://www.codewars.com/kata/simple-fun-number-74-growing-plant");
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

            fileManager.UpdateResourceFile(task);
            fileManager.CopyFilesRecursively(new DirectoryInfo(Environment.CurrentDirectory + ConfigurationManager.AppSettings["source"]),new DirectoryInfo(@"Resource\Test"));

            Console.ReadLine();
        }
    }
}
