using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace ParserService
{
    public class Parser : IParserService
    {
        
        private readonly ChromeDriver _browser;
        public Parser()
        {
            _browser = new ChromeDriver();
            _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }
        public void Authorization(string login = "frieroktut@gmail.com", string password = "nicepassword")
        {
            _browser.Url = "https://www.codewars.com/";
            var element = _browser.FindElement(By.LinkText("Log In"));
            element.Click();
            element = _browser.FindElement(By.Id("user_email"));
            element.SendKeys(login);
            element = _browser.FindElement(By.Id("user_password"));
            element.SendKeys(password);
            element = _browser.FindElement(By.XPath("//*[@id=\"new_user\"]/button"));
            element.Click();
        }

        private string GetPublicTest()
        {
            //IF this task is played, it throws exception
            IWebElement element;
            try
            {
                element = _browser.FindElement(By.XPath("//*[@id=\"play_btn\"]"));
                element.Click();
            }
            catch (Exception e)
            {
                Console.WriteLine("this task was already taken");
                element = _browser.FindElementByXPath("//*[@id=\"replay_btn\"]");
                element.Click();
            }

            
            element = _browser.FindElementByXPath("//*[@id=\"fixture\"]/div[2]/ul/li[2]");
            element.Click();
            element = _browser.FindElementByXPath("//*[@id=\"fixture\"]/div[1]");
            var lines = element.FindElements(By.ClassName("CodeMirror-line"));
            Thread.Sleep(1000);
            var result = string.Join(string.Empty, lines.Select(i => i.Text + "\n"));
            element = _browser.FindElementByXPath("//*[@id=\"fixture\"]/div[2]/ul/li[3]");
            element.Click();
            return result;
        }

        private string GetFirstSolution()
        {
            IWebElement element;
            try
            {
                element = _browser.FindElementById("surrender_btn");
                element.Click();
                element = _browser.FindElementByXPath("//*[@id=\"confirm_modal\"]/div[4]/ul/li[3]/a");
                element.Click();
            }
            catch (Exception e)
            {
                Console.WriteLine("This task was already taken");
            }

            try
            {
                element = _browser.FindElementById("view_solutions");
                element.Click();
            }
            catch (Exception e)
            {
            }

            element = _browser.FindElementById("solutions_list");
            var code = element.FindElement(By.CssSelector("Code"));
            return code.Text;
        }

        private string GetDescription()
        {
            var element = _browser.FindElementByLinkText("Show Kata Description");
            element.Click();
            //wait the animation
            Thread.Sleep(2000);

            element = _browser.FindElementById("description");
            return element.Text;
        }

        private string GetPrivateTest()
        {
            var element = _browser.FindElementByLinkText("Show Kata Test Cases");
            element.Click();
            element = _browser.FindElementById("fixture_panel");
            //wait the animation
            Thread.Sleep(2000);
            var code = element.FindElement(By.CssSelector("code"));
            return code.Text;
        }

        private string GetName(string url)
        {
            return url.Substring(url.LastIndexOf('/') + 1);
        }
        public WarsTask GetTask(string url)
        {
            WarsTask task = new WarsTask();
            _browser.Url = url;
            task.Name = GetName(url);
            task.PublicTest = GetPublicTest();
            task.Solution = GetFirstSolution();
            task.Description = GetDescription();
            task.PrivateTest = GetPrivateTest();
            return task;
        }

        public IEnumerable<WarsTask> GetTasks(IEnumerable<string> urls)
        {
            foreach (var url in urls)
            {
                yield return GetTask(url);
            }
        }
    }
}
