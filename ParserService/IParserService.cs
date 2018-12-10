using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserService
{
    public interface IParserService
    {
        void Authorization(string login = "frieroktut@gmail.com", string password = "nicepassword");
        WarsTask GetTask(string url);
        IEnumerable<WarsTask> GetTasks(IEnumerable<string> urls);
    }
}
