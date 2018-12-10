using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager;
using OpenQA.Selenium.Chrome;
using ParserService;
using ProjectBuilder;
using SimpleInjector;

namespace DependencyResolver
{
    public class Resolver
    {
        public static void Resolve(Container container)
        {
            container.Register<IParserService, Parser>();
            container.Register<IBuildService, BuildService>();
            container.Register<IFileManager,FileManager.FileManager>();
        }
    }
}
